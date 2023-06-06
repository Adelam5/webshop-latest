using Application.Common.Interfaces;
using Application.Common.Interfaces.Repositories;
using Domain.Core.Cart;

namespace Infrastructure.Persistence.Repositories;
internal class CartRepository : ICartRepository
{
    private readonly ICacheService cacheService;

    public CartRepository(ICacheService cacheService)
    {
        this.cacheService = cacheService;
    }

    public async Task<Cart?> GetCartById(string userId)
    {
        var key = $"cart-{userId}";

        var cart = await cacheService.Get<Cart>(key);

        return cart;

    }

    public async Task<Cart> UpdateCart(string userId, Cart cart)
    {
        var key = $"cart-{userId}";

        await cacheService.Set(key, cart);

        return cart;
    }

    public async Task DeleteCart(string userId)
    {
        var key = $"cart-{userId}";

        await cacheService.Remove(key);
    }
}

