using Application.Common.Interfaces.Messaging;

namespace Application.Authentication.Commands.VerifyEmail;
public sealed record VerifyEmailCommand(
    string Token,
    string UserId) : ICommand<bool>;