namespace Application.Orders.Queries.GetById;
public sealed record GetByIdOrderResponse(
    Guid Id,
    string Customer,
    string PaymentStatus,
    decimal Total,
    DateTime CreatedOnUtc,
    DateTime ModifiedOnUtc)
{
    public CustomerAddress Address { get; set; }
    public List<Item> Items { get; set; }
}

public sealed record CustomerAddress(
    string Street,
    string City,
    string State,
    string Zipcode);

public sealed record Item(
    Guid ProductId,
    string Name,
    decimal Price,
    int Quantity);
