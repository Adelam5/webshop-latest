using Application.Common.Interfaces.Messaging;

namespace Application.Customers.Queries.GetById;
public sealed record GetCustomerByIdQuery(Guid Id) : IQuery<GetCustomerByIdResponse>;
