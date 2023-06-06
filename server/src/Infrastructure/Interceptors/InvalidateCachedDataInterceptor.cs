using Application.Common.Interfaces;
using Domain.Primitives;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Infrastructure.Interceptors;
public sealed class InvalidateCachedDataInterceptor
    : SaveChangesInterceptor
{
    private readonly ICacheService cacheService;

    public InvalidateCachedDataInterceptor(ICacheService cacheService)
    {
        this.cacheService = cacheService;
    }

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
    {
        DbContext? dbContext = eventData.Context;

        if (dbContext is null)
        {
            return base.SavingChangesAsync(
                eventData,
                result,
                cancellationToken);
        }

        IEnumerable<EntityEntry<ICacheableEntity>> entries =
            dbContext
                .ChangeTracker
                .Entries<ICacheableEntity>();

        foreach (EntityEntry<ICacheableEntity> entityEntry in entries)
        {
            Type entityType = entityEntry.Entity.GetType();
            string entityName = entityType.Name.ToLower();

            cacheService.Remove($"{entityName}s");

            cacheService.Remove(
                $"{entityName}-{entityEntry.Property(x => x.Id).CurrentValue}");
        }

        return base.SavingChangesAsync(
            eventData,
            result,
            cancellationToken);
    }
}
