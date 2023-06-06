using Application.Common.Interfaces.Messaging;

namespace Application.Authentication.Commands.Register;
public sealed record RegisterUserCommand(
    string FirstName,
    string LastName,
    string Email,
    string Password) : ICommand<string>;