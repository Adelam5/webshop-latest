using Application.Common.Interfaces.Messaging;

namespace Application.Orders.Queries.GetById;

public sealed record GetOrderByIdQuery(Guid Id) : IQuery<GetByIdOrderResponse>;
