using Application.Common.Interfaces.Messaging;

namespace Application.Carts.Commands.UpdateDeliveryMethod;
public sealed record UpdateDeliveryMethodCommand(
    string DeliveryMethodId,
    decimal DeliveryMethodPrice) : ICommand<string>;
