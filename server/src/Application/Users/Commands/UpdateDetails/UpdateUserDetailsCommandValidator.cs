using Domain.Constants;
using FluentValidation;

namespace Application.Users.Commands.UpdateDetails;

public class UpdateUserDetailsCommandValidator : AbstractValidator<UpdateUserDetailsCommand>
{
    public UpdateUserDetailsCommandValidator()
    {
        RuleFor(x => x.FirstName).NotEmpty().Length(CommonConstants.StringMinLength, CommonConstants.StringMaxLength);
        RuleFor(x => x.LastName).NotEmpty().Length(CommonConstants.StringMinLength, CommonConstants.StringMaxLength);
        RuleFor(x => x.Email).NotEmpty().EmailAddress().Length(CommonConstants.StringMinLength, CommonConstants.StringMaxLength);
    }
}

