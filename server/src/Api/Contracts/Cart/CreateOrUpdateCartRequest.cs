namespace Api.Contracts.Cart;

public sealed record CreateOrUpdateCartRequest(string? paymentIntentId, string? clientSecret,
    string? deliveryMethodId, decimal deliveryPrice, decimal subtotal,
    List<CartItemRequest> Items);

public sealed record CartItemRequest(
    string Id,
    int Quantity,
    string Name,
    decimal Price);
