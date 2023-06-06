namespace Api.Contracts.Cart;

public sealed record UpdateDeliveryMethodRequest(
    string DeliveryMethodId,
    decimal DeliveryMethodPrice);
