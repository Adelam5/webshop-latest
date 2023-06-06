namespace Application.Orders.Commands.Create;
public sealed record CreateOrUpdateOrderResponse(
    Guid Id,
    CreateOrUpdateOrderCustomer Customer,
    string PaymentStatus,
    string DeliveryMethodId,
    decimal Subtotal,
    decimal DeliveryMethodPrice,
    decimal Total
    )
{
    public List<CreateOrUpdateOrderItem> Items { get; set; }
};

public sealed record CreateOrUpdateOrderCustomer(
    Guid Id,
    string FirstName,
    string LastName,
    CreateOrUpdateOrderCustomerAddress Address
    );

public sealed record CreateOrUpdateOrderCustomerAddress(
    string Street,
    string City,
    string State,
    string Zipcode);

public sealed record CreateOrUpdateOrderItem(
    Guid ProductId,
    string Name,
    decimal Price,
    int Quantity);