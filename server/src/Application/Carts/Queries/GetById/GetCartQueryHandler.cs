using Application.Common.Interfaces;
using Application.Common.Interfaces.Messaging;
using Application.Common.Interfaces.Repositories;
using AutoMapper;
using Domain.Core.Cart;
using Domain.Errors;
using Domain.Primitives.Result;

namespace Application.Carts.Queries.GetById;
internal sealed class GetCartQueryHandler : IQueryHandler<GetCartQuery, Cart>
{
    private readonly IMapper mapper;
    private readonly ICurrentUserService currentUserService;
    private readonly ICartRepository cartRepository;

    public GetCartQueryHandler(IMapper mapper,
        ICurrentUserService currentUserService,
        ICartRepository cartRepository)
    {
        this.mapper = mapper;
        this.currentUserService = currentUserService;
        this.cartRepository = cartRepository;
    }

    public async Task<Result<Cart>> Handle(GetCartQuery request, CancellationToken cancellationToken)
    {
        var userId = currentUserService.UserId;

        if (string.IsNullOrEmpty(userId))
            return Result.Failure<Cart>(Errors.Authentication.NotAuthenticated);

        var cart = await cartRepository.GetCartById(userId);

        if (cart is null)
        {
            cart = new Cart(userId);
        }

        await cartRepository.UpdateCart(userId, cart);

        return Result.Success(cart);
    }
}
