using Application.Common.Interfaces;
using Application.Common.Interfaces.Messaging;
using Application.Common.Interfaces.Repositories;
using Domain.Errors;
using Domain.Primitives.Result;

namespace Application.Carts.Commands.Delete;

internal class DeleteCartCommandHandler : ICommandHandler<DeleteCartCommand, string>
{
    private readonly ICartRepository cartRepository;
    private readonly ICurrentUserService currentUserService;

    public DeleteCartCommandHandler(ICartRepository cartRepository, ICurrentUserService currentUserService)
    {
        this.cartRepository = cartRepository;
        this.currentUserService = currentUserService;
    }

    public async Task<Result<string>> Handle(DeleteCartCommand request, CancellationToken cancellationToken)
    {
        if (currentUserService.UserId is null)
            return Result.Failure<string>(Errors.Authentication.NotAuthenticated);

        var cart = await cartRepository.GetCartById(currentUserService.UserId);

        if (cart is null)
            return Result.Failure<string>(Errors.Cart.NotFound);

        await cartRepository.DeleteCart(currentUserService.UserId);

        return Result.Success(currentUserService.UserId);
    }
}
