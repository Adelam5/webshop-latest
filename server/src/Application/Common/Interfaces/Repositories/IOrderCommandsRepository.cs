using Domain.Core.Orders;

namespace Application.Common.Interfaces.Repositories;

public interface IOrderCommandsRepository
{
    Task<Order?> GetById(Guid id, CancellationToken cancellationToken = default);
    Task<Order?> GetByPaymentIntentId(string paymentIntentId, CancellationToken cancellationToken = default);
    void Add(Order order);
    void Update(Order order);
    void Remove(Order order);
}
