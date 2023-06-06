using Api.Contracts.User;

namespace Api.Contracts.Customer;

public record UpdateCustomerInfoRequest(
    UpdateUserDetailsRequest UserData,
    AddressRequest Address)
{
    public string Id { get; set; }
};


