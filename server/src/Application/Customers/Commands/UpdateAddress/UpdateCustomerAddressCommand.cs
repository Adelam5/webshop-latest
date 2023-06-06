using Application.Common.Interfaces.Messaging;

namespace Application.Customers.Commands.UpdateAddress;

public sealed record UpdateCustomerAddressCommand(
    Guid CustomerId,
    string Street,
    string City,
    string State,
    string Zipcode) : ICommand<Guid>;