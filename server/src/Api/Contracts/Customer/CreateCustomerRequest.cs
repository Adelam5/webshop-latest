using Api.Contracts.Authentication;

namespace Api.Contracts.Customer;

public record CreateCustomerRequest(
    RegisterUserRequest UserData,
    AddressRequest Address);

