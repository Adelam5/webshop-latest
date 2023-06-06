using Application.Common.Interfaces.Messaging;

namespace Application.Carts.Commands.AddItem;
public sealed record AddItemCommand(
    string Id,
    string Name,
    decimal Price) : ICommand<string>;
