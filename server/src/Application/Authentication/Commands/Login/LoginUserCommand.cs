using Application.Common.Interfaces.Messaging;

namespace Application.Authentication.Commands.Login;
public sealed record LoginUserCommand(string Email, string Password) : ICommand<string>;
