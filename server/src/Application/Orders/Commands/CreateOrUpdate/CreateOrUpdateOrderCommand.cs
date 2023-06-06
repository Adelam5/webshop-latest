using Application.Common.Interfaces.Messaging;

namespace Application.Orders.Commands.Create;

public sealed record CreateOrUpdateOrderCommand() : ICommand<CreateOrUpdateOrderResponse>;
