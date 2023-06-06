using Application.Common.Interfaces.Messaging;

namespace Application.Orders.Queries.GetByCustomerId;
public sealed record GetOrdersByCustomerIdQuery(Guid CustomerId) : IQuery<Guid>;