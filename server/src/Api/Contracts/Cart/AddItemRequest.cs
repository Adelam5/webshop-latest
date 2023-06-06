namespace Api.Contracts.Cart;
public sealed record AddItemRequest(
    string Id,
    string Name,
    decimal Price);
