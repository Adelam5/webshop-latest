using Application.Common.Interfaces.Messaging;

namespace Application.Authentication.Commands.ChangePassword;

public sealed record ChangeUserPasswordCommand(
    string CurrentPassword,
    string NewPassword,
    string ConfirmPassword) : ICommand<bool>;
