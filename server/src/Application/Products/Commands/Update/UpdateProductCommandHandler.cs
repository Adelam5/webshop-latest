using Application.Common.Interfaces;
using Application.Common.Interfaces.Messaging;
using Application.Common.Interfaces.Repositories;
using Domain.Errors;
using Domain.Primitives.Result;

namespace Application.Products.Commands.Update;

internal sealed class UpdateProductCommandHandler : ICommandHandler<UpdateProductCommand, Guid>
{
    private readonly IProductCommandsRepository productRepository;
    private readonly IUnitOfWork unitOfWork;

    public UpdateProductCommandHandler(IProductCommandsRepository productRepository,
        IUnitOfWork unitOfWork)
    {
        this.productRepository = productRepository;
        this.unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var product = await productRepository.GetById(request.Id);

        if (product is null)
            return Result.Failure<Guid>(Errors.Product.NotFound);

        product.UpdateProduct(request.Name, request.Description, request.Price);

        productRepository.Update(product);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(product.Id);

    }
}
