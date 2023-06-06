using Application.Common.Interfaces;
using Application.Common.Interfaces.Messaging;
using Application.Common.Interfaces.Repositories;
using Domain.Core.Products;
using Domain.Primitives.Result;

namespace Application.Products.Commands.Create;
internal sealed class CreateProductCommandHandler : ICommandHandler<CreateProductCommand, Guid>
{
    private readonly IProductCommandsRepository productRepository;
    private readonly IUnitOfWork unitOfWork;

    public CreateProductCommandHandler(IProductCommandsRepository productRepository,
        IUnitOfWork unitOfWork)
    {
        this.productRepository = productRepository;
        this.unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var (name, description, price) = request;

        var newProduct = Product.Create(name, description, price);

        productRepository.Add(newProduct);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(newProduct.Id);
    }
}
