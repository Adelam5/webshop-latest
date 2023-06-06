using Api.Contracts.Authentication;
using Application.Authentication.Commands.ChangePassword;
using Application.Authentication.Commands.ForgotPassword;
using Application.Authentication.Commands.Login;
using Application.Authentication.Commands.Logout;
using Application.Authentication.Commands.Register;
using Application.Authentication.Commands.ResetPassword;
using Application.Authentication.Commands.VerifyEmail;
using Application.Authentication.Queries.GetCurrentUser;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

public class AuthController : ApiController
{

    [HttpGet("me")]
    public async Task<ActionResult> Login(CancellationToken cancellationToken)
    {
        return HandleResult(await Mediator.Send(new GetCurrentUserQuery(), cancellationToken));
    }

    [HttpPost("register")]
    public async Task<ActionResult> Register(RegisterUserRequest registerData, CancellationToken cancellationToken)
    {
        var command = Mapper.Map<RegisterUserCommand>(registerData);

        return HandleResult(await Mediator.Send(command, cancellationToken));
    }

    [HttpPost("login")]
    public async Task<ActionResult> Login(LoginUserRequest registerData, CancellationToken cancellationToken)
    {
        var command = Mapper.Map<LoginUserCommand>(registerData);

        return HandleResult(await Mediator.Send(command, cancellationToken));
    }

    [HttpPost("verify-email")]
    public async Task<ActionResult> VerifyEmail(VerifyEmailRequest request, CancellationToken cancellationToken)
    {
        var command = Mapper.Map<VerifyEmailCommand>(request);

        return HandleResult(await Mediator.Send(command, cancellationToken));
    }

    [HttpPut("change-password")]
    public async Task<ActionResult> ChangePassword(ChangePasswordRequest password, CancellationToken cancellationToken)
    {
        var command = Mapper.Map<ChangeUserPasswordCommand>(password);

        return HandleResult(await Mediator.Send(command, cancellationToken));
    }

    [HttpPost("forgot-password")]
    public async Task<ActionResult> RequestPasswordReset(ForgotPasswordRequest request)
    {
        return HandleResult(await Mediator.Send(new ForgotPasswordCommand(request.Email)));
    }

    [HttpPut("reset-password")]
    public async Task<ActionResult> ResetPassword(ResetPasswordRequest request, CancellationToken cancellationToken)
    {
        var command = Mapper.Map<ResetUserPasswordCommand>(request);

        return HandleResult(await Mediator.Send(command, cancellationToken));
    }

    [HttpPost("logout")]
    public ActionResult Logout()
    {
        Mediator.Send(new LogoutUserCommand());
        return Ok();
    }
}
