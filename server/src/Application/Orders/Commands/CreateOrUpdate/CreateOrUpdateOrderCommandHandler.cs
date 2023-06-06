using Application.Common.Interfaces;
using Application.Common.Interfaces.Messaging;
using Application.Common.Interfaces.Repositories;
using Application.Common.Interfaces.Repositories.Queries;
using AutoMapper;
using Domain.Core.Cart.Entities;
using Domain.Core.OrderAggregate.Entities;
using Domain.Core.Orders;
using Domain.Core.Orders.ValueObjects;
using Domain.Errors;
using Domain.Primitives.Result;

namespace Application.Orders.Commands.Create;

internal sealed class CreateOrUpdateOrderCommandHandler : ICommandHandler<CreateOrUpdateOrderCommand, CreateOrUpdateOrderResponse>
{
    private readonly IMapper mapper;
    private readonly IOrderCommandsRepository orderRepository;
    private readonly ICartRepository cartRepository;
    private readonly ICustomerQueriesRepository customerRepository;
    private readonly IProductQueriesRepository productRepository;
    private readonly ICurrentUserService currentUserService;
    private readonly IDeliveryMethodRepository deliveryMethodRepository;
    private readonly IUnitOfWork unitOfWork;

    public CreateOrUpdateOrderCommandHandler(IMapper mapper, IOrderCommandsRepository orderRepository,
        ICartRepository cartRepository, ICustomerQueriesRepository customerRepository, IProductQueriesRepository productRepository,
        ICurrentUserService currentUserService, IDeliveryMethodRepository deliveryMethodRepository, IUnitOfWork unitOfWork)
    {
        this.mapper = mapper;
        this.orderRepository = orderRepository;
        this.cartRepository = cartRepository;
        this.customerRepository = customerRepository;
        this.productRepository = productRepository;
        this.currentUserService = currentUserService;
        this.deliveryMethodRepository = deliveryMethodRepository;
        this.unitOfWork = unitOfWork;
    }

    public async Task<Result<CreateOrUpdateOrderResponse>> Handle(CreateOrUpdateOrderCommand request, CancellationToken cancellationToken)
    {
        var userId = currentUserService.UserId;
        var userEmail = currentUserService.Email;

        if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(userEmail))
            return Result.Failure<CreateOrUpdateOrderResponse>(Errors.Authentication.NotAuthenticated);

        var cart = await cartRepository.GetCartById(userId);

        if (cart is null)
            return Result.Failure<CreateOrUpdateOrderResponse>(Errors.Cart.NotFound);

        var order = await orderRepository.GetByPaymentIntentId(cart.PaymentIntentId, cancellationToken);

        var deliveryMethod = await deliveryMethodRepository
        .GetById(Guid.Parse(cart.DeliveryMethodId), cancellationToken);

        if (deliveryMethod is null)
            return Result.Failure<CreateOrUpdateOrderResponse>(Errors.Customer.NotFound);

        List<OrderItem> orderItems = new();
        foreach (CartItem item in cart.Items)
        {
            var product = await productRepository.GetById(Guid.Parse(item.Id), cancellationToken);

            if (product is null)
                return Result.Failure<CreateOrUpdateOrderResponse>(Errors.Product.NotFound);

            var orderItem = new OrderItem(product.Id, product.Name, product.Price, item.Quantity, product.PhotoName);

            orderItems.Add(orderItem);
        }

        var orderDeliveryMethod = OrderDeliveryMethod.Create(deliveryMethod.Id, deliveryMethod.Name, deliveryMethod.Price);

        if (order is null)
        {
            var customer = await customerRepository.GetByUserId(userId, cancellationToken);

            if (customer is null)
                return Result.Failure<CreateOrUpdateOrderResponse>(Errors.Customer.NotFound);

            var (street, city, state, zipcode) = customer.Address;

            var orderCustomer = new OrderCustomer(customer.Id, customer.FirstName, customer.LastName,
                street, city, state, zipcode);

            order = Order.Create(userId, orderCustomer, orderItems, cart.PaymentIntentId, orderDeliveryMethod);

            orderRepository.Add(order);
        }
        else
        {
            order = order.Update(orderItems, orderDeliveryMethod);

            orderRepository.Update(order);
        }

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return mapper.Map<CreateOrUpdateOrderResponse>(order);
    }
}
