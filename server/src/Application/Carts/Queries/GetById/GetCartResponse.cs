using Application.Common.Interfaces.Messaging;

namespace Application.Carts.Queries.GetById;
public record GetCartResponse(
    string PaymentIntentId,
    string ClientSecret,
    Guid? DeliveryMethodId,
    IReadOnlyList<CartItemResponse> CartItems) : ICommand<string>;

public sealed record CartItemResponse(
    string Id,
    int Quantity,
    string Name,
    decimal Price);
