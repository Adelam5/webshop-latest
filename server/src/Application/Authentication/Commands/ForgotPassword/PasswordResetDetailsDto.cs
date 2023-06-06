namespace Application.Authentication.Commands.ForgotPassword;
public sealed record PasswordResetDetailsDto(
    string UserId,
    string Token);
