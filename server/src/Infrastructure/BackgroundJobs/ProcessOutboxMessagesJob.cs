using Domain.Abstractions.Interfaces;
using Domain.Primitives;
using Infrastructure.Outbox;
using Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Polly;
using Polly.Retry;
using Quartz;

namespace Infrastructure.BackgroundJobs;

[DisallowConcurrentExecution]
public class ProcessOutboxMessagesJob : IJob
{
    private readonly ILogger<ProcessOutboxMessagesJob> logger;
    private readonly IDateTimeProvider dateTimeProvider;
    private readonly DataContext dataContext;
    private readonly IPublisher publisher;

    public ProcessOutboxMessagesJob(ILogger<ProcessOutboxMessagesJob> logger, IDateTimeProvider dateTimeProvider,
        DataContext dataContext, IPublisher publisher)
    {
        this.logger = logger;
        this.dateTimeProvider = dateTimeProvider;
        this.dataContext = dataContext;
        this.publisher = publisher;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        List<OutboxMessage> messages = await dataContext
                    .Set<OutboxMessage>()
                    .Where(m => m.ProcessedOnUtc == null || m.Error != null)
                    .OrderBy(m => m.OccurredOnUtc)
                    .Take(20)
                    .ToListAsync(context.CancellationToken);

        foreach (OutboxMessage outboxMessage in messages)
        {
            IDomainEvent? domainEvent = JsonConvert
                .DeserializeObject<IDomainEvent>(
                    outboxMessage.Content,
                    new JsonSerializerSettings
                    {
                        TypeNameHandling = TypeNameHandling.All
                    });

            if (domainEvent is null)
            {
                logger.LogError("Failed to deserialize domain event with OutboxMessage Id: {OutboxMessageId}. Content: {Content}",
                    outboxMessage.Id, outboxMessage.Content);
                continue;
            }

            AsyncRetryPolicy policy = Policy
                .Handle<Exception>()
                .WaitAndRetryAsync(3, attempt => TimeSpan.FromMilliseconds(50 * attempt));

            PolicyResult result = await policy.ExecuteAndCaptureAsync(() =>
                publisher.Publish(domainEvent, context.CancellationToken));

            if (result.FinalException != null)
            {
                logger.LogError(result.FinalException, "Error occurred while processing outbox message {MessageId}.",
                    outboxMessage.Id);
            }

            outboxMessage.Error = result.FinalException?.ToString();
            outboxMessage.ProcessedOnUtc = dateTimeProvider.UtcNow;

        }
        await dataContext.SaveChangesAsync();
    }
}

