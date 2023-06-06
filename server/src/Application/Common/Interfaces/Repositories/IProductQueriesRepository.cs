using Application.Products.Queries.GetById;
using Application.Products.Queries.List;

namespace Application.Common.Interfaces.Repositories;
public interface IProductQueriesRepository
{
    Task<List<ListProductsResponse>> GetAll(CancellationToken cancellationToken = default);
    Task<GetProductByIdResponse?> GetById(Guid id, CancellationToken cancellationToken = default);
}
