using Application.Authentication.Commands.ChangePassword;
using Application.Authentication.Commands.ForgotPassword;
using Application.Authentication.Commands.Login;
using Application.Authentication.Commands.Register;
using Application.Authentication.Commands.ResetPassword;
using Application.Authentication.Queries.GetCurrentUser;
using Domain.Primitives.Result;

namespace Application.Common.Interfaces;
public interface IAuthService
{
    Task<GetCurrentUserResponse?> GetCurrentUser(string userId, CancellationToken cancellationToken = default);
    Task<Result<string>> Login(LoginUserCommand loginData, CancellationToken cancellationToken = default);
    Task<Result<string>> Register(RegisterUserCommand registerData, CancellationToken cancellationToken = default);
    Task<Result<bool>> VerifyEmail(string token, string userId);
    Task<Result<bool>> ChangePassword(string userId, ChangeUserPasswordCommand password);
    Task<PasswordResetDetailsDto?> GetPasswordResetDetails(string email);
    Task<Result<bool>> ResetPassword(ResetUserPasswordCommand request);
    void Logout();
}
