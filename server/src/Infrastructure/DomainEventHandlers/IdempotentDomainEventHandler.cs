using Application.Common.Interfaces.Messaging;
using Domain.Primitives;
using Infrastructure.Outbox;
using Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Idempotence;
public sealed class IdempotentDomainEventHandler<TDomainEvent> : IDomainEventHandler<TDomainEvent>
    where TDomainEvent : IDomainEvent
{
    private readonly INotificationHandler<TDomainEvent> decorated;
    private readonly DataContext context;

    public IdempotentDomainEventHandler(INotificationHandler<TDomainEvent> decorated, DataContext context)
    {
        this.decorated = decorated;
        this.context = context;
    }

    public async Task Handle(TDomainEvent notification, CancellationToken cancellationToken)
    {
        string consumer = decorated.GetType().Name;

        if (await context.Set<OutboxMessageConsumer>()
                .AnyAsync(
                    outboxMessageConsumer =>
                        outboxMessageConsumer.Id == notification.Id &&
                        outboxMessageConsumer.Name == consumer,
                    cancellationToken))
        {
            return;
        }

        await decorated.Handle(notification, cancellationToken);

        context.Set<OutboxMessageConsumer>()
            .Add(new OutboxMessageConsumer
            {
                Id = notification.Id,
                Name = consumer
            });

        await context.SaveChangesAsync(cancellationToken);
    }
}
