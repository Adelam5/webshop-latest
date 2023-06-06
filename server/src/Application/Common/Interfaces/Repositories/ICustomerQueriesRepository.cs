using Application.Customers.Queries.GetById;
using Application.Customers.Queries.List;

namespace Application.Common.Interfaces.Repositories.Queries;
public interface ICustomerQueriesRepository
{
    Task<List<ListCustomerDto>> GetAll(CancellationToken cancellationToken = default);
    Task<GetCustomerByIdResponse?> GetById(Guid id, CancellationToken cancellationToken = default);
    Task<GetCustomerByIdResponse?> GetByUserId(string id, CancellationToken cancellationToken = default);
}
