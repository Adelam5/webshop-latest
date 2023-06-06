using Domain.Core.Cart;

namespace Application.Common.Interfaces.Repositories;
public interface ICartRepository
{
    Task<Cart?> GetCartById(string userId);
    Task<Cart> UpdateCart(string userId, Cart cart);
    Task DeleteCart(string userId);
}
