using Application.Common.Interfaces.Messaging;

namespace Application.Customers.Commands.Delete;
public sealed record DeleteCustomerCommand(
    Guid Id) : ICommand<Guid>;
