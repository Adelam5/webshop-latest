using Application.Common.Interfaces;
using Application.Common.Interfaces.Messaging;
using Domain.Errors;
using Domain.Primitives.Result;

namespace Application.Authentication.Commands.ForgotPassword;
internal sealed class ForgotPasswordCommandHandler : ICommandHandler<ForgotPasswordCommand, bool>
{
    private readonly IAuthService authService;
    private readonly IEmailSender emailSender;

    public ForgotPasswordCommandHandler(IAuthService authService, IEmailSender emailSender)
    {
        this.authService = authService;
        this.emailSender = emailSender;
    }

    public async Task<Result<bool>> Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
    {
        var details = await authService.GetPasswordResetDetails(request.Email);

        if (details is null)
            return Result.Failure<bool>(Errors.User.NotFound);

        var result = await emailSender.SendResetPasswordEmail(details.UserId, request.Email, details.Token);

        if (!result)
            return Result.Failure<bool>(Errors.Email.SendingFailure(request.Email));

        return true;
    }
}
