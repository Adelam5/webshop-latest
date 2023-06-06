using Application.Common.Interfaces.Messaging;

namespace Application.Authentication.Commands.ResetPassword;
public sealed record ResetUserPasswordCommand(
    string UserId,
    string Token,
    string NewPassword,
    string ConfirmPassword) : ICommand<bool>;
