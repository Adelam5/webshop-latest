using Application.Common.Interfaces;
using Application.Common.Interfaces.Messaging;
using Application.Common.Interfaces.Repositories;
using Domain.Errors;
using Domain.Primitives.Result;

namespace Application.Products.Commands.Delete;
internal sealed class DeleteProductCommandHandler : ICommandHandler<DeleteProductCommand, Guid>
{
    private readonly IProductCommandsRepository productRepository;
    private readonly IUnitOfWork unitOfWork;

    public DeleteProductCommandHandler(IProductCommandsRepository productRepository,
        IUnitOfWork unitOfWork)
    {
        this.productRepository = productRepository;
        this.unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid>> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var product = await productRepository.GetById(request.Id, cancellationToken);

        if (product is null)
            return Result.Failure<Guid>(Errors.Product.NotFound);

        product.MarkAsDeleted();

        productRepository.Remove(product);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(product.Id);
    }
}
