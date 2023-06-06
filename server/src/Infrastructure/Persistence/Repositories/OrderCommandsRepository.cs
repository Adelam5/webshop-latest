using Application.Common.Interfaces.Repositories;
using Domain.Core.Orders;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories.Commands;

internal class OrderCommandsRepository : IOrderCommandsRepository
{
    private readonly DataContext dataContext;

    public OrderCommandsRepository(DataContext dataContext)
    {
        this.dataContext = dataContext;
    }

    public async Task<Order?> GetById(Guid id, CancellationToken cancellationToken = default) =>
        await dataContext
            .Set<Order>()
            .FirstOrDefaultAsync(order => order.Id == id, cancellationToken);

    public async Task<Order?> GetByPaymentIntentId(string paymentIntentId, CancellationToken cancellationToken = default) =>
    await dataContext
        .Set<Order>()
        .FirstOrDefaultAsync(order => order.PaymentIntentId == paymentIntentId, cancellationToken);

    public void Add(Order order) =>
        dataContext.Set<Order>().Add(order);

    public void Update(Order order) =>
        dataContext.Set<Order>().Update(order);

    public void Remove(Order order) =>
        dataContext.Set<Order>().Remove(order);
}
