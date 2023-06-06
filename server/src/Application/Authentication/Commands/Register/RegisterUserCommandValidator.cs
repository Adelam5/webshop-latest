using Domain.Constants;
using FluentValidation;

namespace Application.Authentication.Commands.Register;
public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
{
    public RegisterUserCommandValidator()
    {
        RuleFor(x => x.FirstName).NotEmpty().Length(CommonConstants.StringMinLength, CommonConstants.StringMaxLength);
        RuleFor(x => x.LastName).NotEmpty().Length(CommonConstants.StringMinLength, CommonConstants.StringMaxLength);
        RuleFor(x => x.Email).NotEmpty().EmailAddress().Length(CommonConstants.StringMinLength, CommonConstants.StringMaxLength);
        RuleFor(x => x.Password).NotEmpty().Length(CommonConstants.PasswordMinLength, CommonConstants.PasswordMaxLength);
    }
}