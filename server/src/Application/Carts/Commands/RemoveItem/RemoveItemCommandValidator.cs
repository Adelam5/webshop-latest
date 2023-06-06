using Domain.Constants;
using FluentValidation;

namespace Application.Carts.Commands.RemoveItem;
public class RemoveItemCommandValidator : AbstractValidator<RemoveItemCommand>
{
    public RemoveItemCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty().Length(CommonConstants.IdMinLength, CommonConstants.IdMaxLength);
    }
}
