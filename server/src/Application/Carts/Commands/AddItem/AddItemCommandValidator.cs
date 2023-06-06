using Domain.Constants;
using FluentValidation;

namespace Application.Carts.Commands.AddItem;
public class AddItemCommandValidator : AbstractValidator<AddItemCommand>
{
    public AddItemCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty().Length(CommonConstants.IdMinLength, CommonConstants.IdMaxLength);
        RuleFor(c => c.Name).NotEmpty().Length(CommonConstants.StringMinLength, CommonConstants.StringMaxLength);
        RuleFor(c => c.Price)
              .NotEmpty()
              .GreaterThan(PriceConstants.MinValue)
              .LessThan(PriceConstants.MaxValue)
              .PrecisionScale(PriceConstants.Precision, PriceConstants.Scale, true);
    }
}
