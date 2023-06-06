using Domain.Primitives;
using MediatR;

namespace Application.Common.Interfaces.Messaging;
public interface IDomainEventHandler<TEvent> : INotificationHandler<TEvent>
    where TEvent : IDomainEvent
{
}
