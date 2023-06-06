using Domain.Constants;
using FluentValidation;

namespace Application.Carts.Commands.CreateOrUpdate;

public class CreateOrUpdateCartCommandValidator : AbstractValidator<CreateOrUpdateCartCommand>
{
    public CreateOrUpdateCartCommandValidator()
    {
        RuleForEach(x => x.Items).ChildRules(child =>
        {
            child.RuleFor(c => c.Id).NotEmpty().Length(CommonConstants.IdMinLength, CommonConstants.IdMaxLength);
            child.RuleFor(c => c.Quantity).NotNull().GreaterThan(0);
            child.RuleFor(c => c.Name).NotEmpty().Length(CommonConstants.StringMinLength, CommonConstants.StringMaxLength);
            child.RuleFor(c => c.Price)
                .NotEmpty()
                .GreaterThan(PriceConstants.MinValue)
                .LessThan(PriceConstants.MaxValue)
                .PrecisionScale(PriceConstants.Precision, PriceConstants.Scale, true);
        });
    }
}
