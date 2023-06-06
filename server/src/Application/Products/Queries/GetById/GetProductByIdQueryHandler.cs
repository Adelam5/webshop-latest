using Application.Common.Interfaces.Messaging;
using Application.Common.Interfaces.Repositories;
using Domain.Errors;
using Domain.Primitives.Result;

namespace Application.Products.Queries.GetById;
internal sealed class GetProductByIdQueryHandler : IQueryHandler<GetProductByIdQuery, GetProductByIdResponse>
{
    private readonly IProductQueriesRepository productRepository;

    public GetProductByIdQueryHandler(IProductQueriesRepository productRepository)
    {
        this.productRepository = productRepository;
    }

    public async Task<Result<GetProductByIdResponse>> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var product = await productRepository.GetById(request.Id, cancellationToken);

        if (product is null)
            return Result.Failure<GetProductByIdResponse>(Errors.Product.NotFound);

        return Result.Success(product);
    }
}
