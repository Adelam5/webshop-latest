using Application.Common.Interfaces.Messaging;

namespace Application.Orders.Queries.GetCurrentUserOrders;

public sealed record GetCurrentUserOrdersQuery() : IQuery<List<GetCurrentUserOrdersResponse>>;
