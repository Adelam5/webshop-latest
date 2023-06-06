using Application.Common.Interfaces;
using Application.Common.Interfaces.Messaging;
using Application.Common.Interfaces.Repositories;
using Domain.Core.Cart;
using Domain.Errors;
using Domain.Primitives.Result;

namespace Application.Carts.Commands.AddItem;
internal sealed class AddItemCommandHandler : ICommandHandler<AddItemCommand, string>
{
    private readonly ICartRepository cartRepository;
    private readonly ICurrentUserService currentUserService;

    public AddItemCommandHandler(ICartRepository cartRepository, ICurrentUserService currentUserService)
    {
        this.cartRepository = cartRepository;
        this.currentUserService = currentUserService;
    }
    public async Task<Result<string>> Handle(AddItemCommand request, CancellationToken cancellationToken)
    {
        var userId = currentUserService.UserId;

        if (string.IsNullOrEmpty(userId))
            return Result.Failure<string>(Errors.Authentication.NotAuthenticated);

        var cart = await cartRepository.GetCartById(userId)
            ?? new Cart(userId);

        cart.AddItem(request.Id, request.Name, request.Price);

        await cartRepository.UpdateCart(userId, cart);

        return userId;
    }
}
