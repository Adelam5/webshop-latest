namespace Application.Customers.Queries.GetById;
public record GetCustomerByIdResponse(
    Guid Id,
    string FirstName,
    string LastName,
    string Email)
{
    public CustomerAddress Address { get; set; } = null!;
}

public record CustomerAddress(
    string Street,
    string City,
    string State,
    string Zipcode);
