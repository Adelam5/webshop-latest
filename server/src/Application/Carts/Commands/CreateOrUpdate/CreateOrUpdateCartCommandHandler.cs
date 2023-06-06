using Application.Common.Interfaces;
using Application.Common.Interfaces.Messaging;
using Application.Common.Interfaces.Repositories;
using AutoMapper;
using Domain.Core.Cart;
using Domain.Errors;
using Domain.Primitives.Result;

namespace Application.Carts.Commands.CreateOrUpdate;
internal sealed class CreateOrUpdateCartCommandHandler : ICommandHandler<CreateOrUpdateCartCommand, string>
{
    private readonly IMapper mapper;
    private readonly ICurrentUserService currentUserService;
    private readonly ICartRepository cartRepository;

    public CreateOrUpdateCartCommandHandler(IMapper mapper,
        ICurrentUserService currentUserService,
        ICartRepository cartRepository)
    {
        this.mapper = mapper;
        this.currentUserService = currentUserService;
        this.cartRepository = cartRepository;
    }

    public async Task<Result<string>> Handle(CreateOrUpdateCartCommand request, CancellationToken cancellationToken)
    {
        var userId = currentUserService.UserId;

        if (string.IsNullOrEmpty(userId))
            return Result.Failure<string>(Errors.Authentication.NotAuthenticated);

        var cart = await cartRepository.GetCartById(userId);

        if (cart is null)
        {
            cart = new Cart(userId);
        }

        cart.ClearItems();

        foreach (var item in request.Items)
        {
            cart.AddItems(item.Id, item.Quantity, item.Name, item.Price);
        }

        if (!string.IsNullOrEmpty(request.DeliveryMethodId) && request.DeliveryMethodId != cart.DeliveryMethodId)
            cart.UpdateDeliveryMethod(request.DeliveryMethodId, request.DeliveryPrice);

        await cartRepository.UpdateCart(userId, cart);

        return Result.Success(userId);
    }
}
