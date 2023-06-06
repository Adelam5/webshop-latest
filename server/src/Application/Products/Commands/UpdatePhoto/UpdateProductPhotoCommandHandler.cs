using Application.Common.Interfaces;
using Application.Common.Interfaces.Messaging;
using Application.Common.Interfaces.Repositories;
using Domain.Abstractions.Interfaces;
using Domain.Constants;
using Domain.Errors;
using Domain.Primitives.Result;

namespace Application.Products.Commands.UpdatePhoto;
internal sealed class UpdateProductPhotoCommandHandler : ICommandHandler<UpdateProductPhotoCommand, Guid>
{
    private readonly IProductCommandsRepository productRepository;
    private readonly IUnitOfWork unitOfWork;
    private readonly IS3Service s3Service;
    private readonly IDateTimeProvider dateTimeProvider;

    public UpdateProductPhotoCommandHandler(IProductCommandsRepository productRepository, IUnitOfWork unitOfWork,
        IS3Service s3Service, IDateTimeProvider dateTimeProvider)
    {
        this.productRepository = productRepository;
        this.unitOfWork = unitOfWork;
        this.s3Service = s3Service;
        this.dateTimeProvider = dateTimeProvider;
    }
    public async Task<Result<Guid>> Handle(UpdateProductPhotoCommand request, CancellationToken cancellationToken)
    {
        var product = await productRepository.GetById(request.ProductId, cancellationToken);

        if (product is null)
            return Result.Failure<Guid>(Errors.Product.NotFound);

        if (product.Photo.Name != PhotoConstants.DefaultPhotoName)
            await s3Service.DeleteFile(product.Photo.Name);

        var uniqueName = dateTimeProvider.UtcNow.ToString("yyyyMMddHHmmss") + request.Photo.FileName;
        var newUrl = await s3Service.UploadFile(request.Photo.OpenReadStream(), uniqueName);

        product.UpdatePhoto(newUrl, uniqueName);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(product.Id);
    }
}
