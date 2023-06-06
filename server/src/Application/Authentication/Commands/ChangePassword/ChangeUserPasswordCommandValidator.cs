using Domain.Constants;
using FluentValidation;

namespace Application.Authentication.Commands.ChangePassword;
public class ChangeUserPasswordCommandValidator : AbstractValidator<ChangeUserPasswordCommand>
{
    public ChangeUserPasswordCommandValidator()
    {
        RuleFor(x => x.CurrentPassword).NotEmpty().Length(CommonConstants.PasswordMinLength, CommonConstants.PasswordMaxLength);
        RuleFor(x => x.NewPassword).NotEmpty().Length(CommonConstants.PasswordMinLength, CommonConstants.PasswordMaxLength);
        RuleFor(x => x.ConfirmPassword).NotEmpty().Equal(x => x.NewPassword);
    }
}