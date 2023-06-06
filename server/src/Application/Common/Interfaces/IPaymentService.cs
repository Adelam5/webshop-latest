using Domain.Core.Cart;
using Domain.Core.Orders;

namespace Application.Common.Interfaces.Services;
public interface IPaymentService
{
    Task<Cart?> CreateOrUpdatePaymentIntent(string userId);
    Task<Order> UpdateOrderPaymentSucceeded(string paymentIntentId);
    Task<Order> UpdateOrderPaymentFailed(string paymentIntentId);
}
