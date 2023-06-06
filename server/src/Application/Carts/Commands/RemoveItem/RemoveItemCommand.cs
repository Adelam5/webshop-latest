using Application.Common.Interfaces.Messaging;

namespace Application.Carts.Commands.RemoveItem;
public sealed record RemoveItemCommand(
    string Id) : ICommand<string>;
