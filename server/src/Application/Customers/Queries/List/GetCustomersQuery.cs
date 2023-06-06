using Application.Common.Interfaces.Messaging;

namespace Application.Customers.Queries.List;
public sealed record GetCustomersQuery() : IQuery<List<ListCustomerDto>>
{
}
