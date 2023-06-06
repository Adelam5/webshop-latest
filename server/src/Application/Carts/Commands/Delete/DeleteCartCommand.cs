using Application.Common.Interfaces.Messaging;

namespace Application.Carts.Commands.Delete;

public sealed record DeleteCartCommand() : ICommand<string>;
