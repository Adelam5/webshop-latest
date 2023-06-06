using Domain.Constants;
using FluentValidation;

namespace Application.Authentication.Commands.ResetPassword;

public class ResetUserPasswordCommandValidator : AbstractValidator<ResetUserPasswordCommand>
{
    public ResetUserPasswordCommandValidator()
    {
        RuleFor(x => x.UserId).NotEmpty().Length(CommonConstants.IdMinLength, CommonConstants.IdMaxLength);
        RuleFor(x => x.Token).NotEmpty();
        RuleFor(x => x.NewPassword).NotEmpty().Length(CommonConstants.PasswordMinLength, CommonConstants.PasswordMaxLength);
        RuleFor(x => x.ConfirmPassword).NotEmpty().Length(CommonConstants.PasswordMinLength, CommonConstants.PasswordMaxLength).Equal(x => x.NewPassword);
    }
}