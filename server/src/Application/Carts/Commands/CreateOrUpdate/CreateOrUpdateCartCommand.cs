using Application.Common.Interfaces.Messaging;

namespace Application.Carts.Commands.CreateOrUpdate;
public sealed record CreateOrUpdateCartCommand(
    string? PaymentIntentId,
    string? ClientSecret,
    string DeliveryMethodId,
    decimal DeliveryPrice,
    decimal Subtotal,
    IReadOnlyList<CartItemCommand> Items) : ICommand<string>;

public sealed record CartItemCommand(
    string Id,
    int Quantity,
    string Name,
    decimal Price);

