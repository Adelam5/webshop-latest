using Application.Common.Interfaces;
using Application.Common.Interfaces.Messaging;
using Domain.Constants;
using Domain.Core.Products.Events;

namespace Application.Products.Events;
internal sealed class DeleteProductPhotoWhenProductDeletedDomainEventHandler
    : IDomainEventHandler<ProductDeletedDomainEvent>
{
    private readonly IS3Service s3Service;

    public DeleteProductPhotoWhenProductDeletedDomainEventHandler(
        IS3Service s3Service)
    {
        this.s3Service = s3Service;
    }

    public async Task Handle(ProductDeletedDomainEvent notification, CancellationToken cancellationToken)
    {
        if (notification.PhotoName != PhotoConstants.DefaultPhotoName)
            await s3Service.DeleteFile(notification.PhotoName);
    }
}
