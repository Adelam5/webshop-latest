using Application.Authentication.Commands.Register;
using Application.Common.Interfaces.Messaging;

namespace Application.Customers.Commands.Create;

public sealed record CreateCustomerCommand(
    RegisterUserCommand UserData,
    AddressCommand Address) : ICommand<Guid>;

public sealed record AddressCommand(
    string Street,
    string City,
    string State,
    string Zipcode);