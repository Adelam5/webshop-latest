namespace Application.Orders.Queries.GetCurrentUserOrders;

public sealed record GetCurrentUserOrdersResponse(
    Guid Id,
    string PaymentStatus,
    decimal Total,
    DateTime CreatedOnUtc,
    DateTime ModifiedOnUtc);