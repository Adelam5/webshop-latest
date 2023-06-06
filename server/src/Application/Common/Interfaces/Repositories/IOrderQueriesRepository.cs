using Application.Orders.Queries.GetById;
using Application.Orders.Queries.GetCurrentUserOrders;

namespace Application.Common.Interfaces.Repositories;
public interface IOrderQueriesRepository
{
    Task<GetByIdOrderResponse?> GetById(Guid id);
    Task<List<GetByIdOrderResponse>> GetByCustomer(Guid customerId);
    Task<List<GetCurrentUserOrdersResponse>> GetCurrentUserOrders(string userId);
}
