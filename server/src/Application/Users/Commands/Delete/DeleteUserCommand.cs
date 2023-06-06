using Application.Common.Interfaces.Messaging;

namespace Application.Users.Commands.Delete;
public sealed record DeleteUserCommand(string Id) : ICommand<string>;
