using Application.Common.Interfaces.Messaging;
using Application.Common.Interfaces.Repositories;
using Domain.Primitives.Result;

namespace Application.Products.Queries.List;
internal sealed class ListProductsQueryHandler : IQueryHandler<ListProductsQuery, List<ListProductsResponse>>
{
    private readonly IProductQueriesRepository productRepository;

    public ListProductsQueryHandler(IProductQueriesRepository productRepository)
    {
        this.productRepository = productRepository;
    }

    public async Task<Result<List<ListProductsResponse>>> Handle(ListProductsQuery request, CancellationToken cancellationToken)
    {
        var products = await productRepository.GetAll(cancellationToken);

        return Result.Success(products);
    }
}
