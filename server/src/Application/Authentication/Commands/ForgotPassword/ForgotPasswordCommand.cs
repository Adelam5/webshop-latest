using Application.Common.Interfaces.Messaging;

namespace Application.Authentication.Commands.ForgotPassword;
public sealed record ForgotPasswordCommand(string Email) : ICommand<bool>;