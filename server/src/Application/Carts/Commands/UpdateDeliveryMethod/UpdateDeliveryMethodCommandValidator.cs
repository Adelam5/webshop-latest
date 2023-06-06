using Domain.Constants;
using FluentValidation;

namespace Application.Carts.Commands.UpdateDeliveryMethod;
internal class UpdateDeliveryMethodCommandValidator : AbstractValidator<UpdateDeliveryMethodCommand>
{
    public UpdateDeliveryMethodCommandValidator()
    {
        RuleFor(c => c.DeliveryMethodId).NotEmpty().Length(CommonConstants.IdMinLength, CommonConstants.IdMaxLength);
        RuleFor(c => c.DeliveryMethodPrice)
            .NotEmpty()
            .GreaterThan(PriceConstants.MinValue)
            .LessThan(PriceConstants.MaxValue)
            .PrecisionScale(PriceConstants.Precision, PriceConstants.Scale, true);
    }
}
