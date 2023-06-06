using Application.Common.Interfaces.Messaging;

namespace Application.Users.Commands.UpdateDetails;

public sealed record UpdateUserDetailsCommand(
    string Id,
    string FirstName,
    string LastName,
    string Email) : ICommand<string>;
