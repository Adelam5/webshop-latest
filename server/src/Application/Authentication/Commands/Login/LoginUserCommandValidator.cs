using Domain.Constants;
using FluentValidation;

namespace Application.Authentication.Commands.Login;
public class LoginUserCommandValidator : AbstractValidator<LoginUserCommand>
{
    public LoginUserCommandValidator()
    {
        RuleFor(x => x.Email).NotEmpty().EmailAddress().Length(CommonConstants.StringMinLength, CommonConstants.StringMaxLength);
        RuleFor(x => x.Password).NotEmpty();
    }
}
