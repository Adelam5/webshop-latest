using Domain.Constants;
using FluentValidation;

namespace Application.Products.Commands.Create;

public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty().Length(CommonConstants.StringMinLength, CommonConstants.StringMaxLength);
        RuleFor(x => x.Description).NotEmpty().Length(CommonConstants.TextMinLength, CommonConstants.TextMaxLength);
        RuleFor(x => x.Price)
                .NotEmpty()
                .GreaterThan(PriceConstants.MinValue)
                .LessThan(PriceConstants.MaxValue)
                .PrecisionScale(PriceConstants.Precision, PriceConstants.Scale, true);
    }
}