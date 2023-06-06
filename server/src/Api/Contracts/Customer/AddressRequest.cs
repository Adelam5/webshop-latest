namespace Api.Contracts.Customer;

public sealed record AddressRequest(
    string Street,
    string City,
    string State,
    string Zipcode);