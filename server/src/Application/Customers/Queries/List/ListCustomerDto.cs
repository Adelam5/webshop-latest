namespace Application.Customers.Queries.List;

public record ListCustomerDto(
    Guid Id,
    string FirstName,
    string LastName,
    string Email,
    string Street,
    string City,
    string State,
    string Zipcode);
