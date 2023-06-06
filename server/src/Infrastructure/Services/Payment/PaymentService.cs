using Application.Common.Interfaces;
using Application.Common.Interfaces.Repositories;
using Application.Common.Interfaces.Services;
using Domain.Core.Cart;
using Domain.Core.Cart.Entities;
using Domain.Core.Orders;
using Domain.Exceptions;
using Microsoft.Extensions.Options;
using Stripe;

namespace Infrastructure.Services.Payment;
public class PaymentService : IPaymentService
{
    private readonly StripeSettings stripeSettings;
    private readonly ICartRepository cartRepository;
    private readonly IProductQueriesRepository productRepository;
    private readonly IDeliveryMethodRepository deliveryMethodRepository;
    private readonly IOrderCommandsRepository orderRepository;
    private readonly IUnitOfWork unitOfWork;

    public PaymentService(IOptions<StripeSettings> options, ICartRepository cartRepository,
    IProductQueriesRepository productRepository, IDeliveryMethodRepository deliveryMethodRepository,
    IOrderCommandsRepository orderRepository, IUnitOfWork unitOfWork
      )
    {
        stripeSettings = options.Value;
        this.cartRepository = cartRepository;
        this.productRepository = productRepository;
        this.deliveryMethodRepository = deliveryMethodRepository;
        this.orderRepository = orderRepository;
        this.unitOfWork = unitOfWork;
    }
    public async Task<Cart?> CreateOrUpdatePaymentIntent(string userId)
    {
        StripeConfiguration.ApiKey = stripeSettings.SecretKey;

        var cart = await cartRepository.GetCartById(userId);

        if (cart is null) return null;

        foreach (CartItem item in cart.Items)
        {
            var productItem = await productRepository.GetById(Guid.Parse(item.Id));
            if (productItem is null) return null;
            if (productItem.Price != item.Price)
            {
                item.Price = productItem.Price;
                item.Name = productItem.Name;
            }
        }

        var deliveryMethod = await deliveryMethodRepository.GetById(Guid.Parse(cart.DeliveryMethodId));

        decimal deliveryPrice;

        if (deliveryMethod is not null)
        {
            deliveryPrice = deliveryMethod.Price;
        }
        else
        {
            deliveryPrice = 0m;
        }

        var subtotal = cart.Items.Sum(i => i.Quantity * i.Price);

        var service = new PaymentIntentService();
        PaymentIntent intent;

        if (string.IsNullOrEmpty(cart.PaymentIntentId))
        {
            var options = new PaymentIntentCreateOptions
            {
                Amount = (long)subtotal * 100 + (long)deliveryPrice * 100,
                Currency = "usd",
                PaymentMethodTypes = new List<string> { "card" }
            };
            intent = await service.CreateAsync(options);
            cart.PaymentIntentId = intent.Id;
            cart.ClientSecret = intent.ClientSecret;
        }
        else
        {
            var options = new PaymentIntentUpdateOptions
            {
                Amount = (long)subtotal * 100 + (long)deliveryPrice * 100
            };
            await service.UpdateAsync(cart.PaymentIntentId, options);
        }

        cart.SetPrice(subtotal, deliveryPrice);

        await cartRepository.UpdateCart(userId, cart);

        return cart;
    }

    public async Task<Order> UpdateOrderPaymentSucceeded(string paymentIntentId)
    {
        var order = await orderRepository.GetByPaymentIntentId(paymentIntentId);

        if (order is null)
            throw new AppException($"Order with paymentIntentId: {paymentIntentId} does not exist.");

        await cartRepository.DeleteCart(order.UserId);

        order.PaymentSucceeded();

        await unitOfWork.SaveChangesAsync();

        return order;
    }

    public async Task<Order> UpdateOrderPaymentFailed(string paymentIntentId)
    {
        var order = await orderRepository.GetByPaymentIntentId(paymentIntentId);

        if (order is null)
            throw new AppException($"Order with paymentIntentId: {paymentIntentId} does not exist.");

        order.PaymentFailed();

        await unitOfWork.SaveChangesAsync();

        return order;
    }
}
