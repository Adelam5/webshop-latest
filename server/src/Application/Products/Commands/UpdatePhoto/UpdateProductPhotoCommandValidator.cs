using Domain.Constants;
using FluentValidation;

namespace Application.Products.Commands.UpdatePhoto;
public class UpdateProductPhotoCommandValidator : AbstractValidator<UpdateProductPhotoCommand>
{
    public UpdateProductPhotoCommandValidator()
    {
        RuleFor(x => x.Photo.Length).NotNull()
                .LessThanOrEqualTo(PhotoConstants.MaxPhotoSizeInBytes)
                .WithMessage("Photo size is larger than allowed");

        RuleFor(x => x.Photo.ContentType).NotNull()
            .Must(x => x.Equals(PhotoConstants.PngContentType)
                    || x.Equals(PhotoConstants.JpgContentType)
                    || x.Equals(PhotoConstants.JpegContentType))
            .WithMessage("File type is not allowed. Please upload a file in JPG or PNG format.");
    }
}
