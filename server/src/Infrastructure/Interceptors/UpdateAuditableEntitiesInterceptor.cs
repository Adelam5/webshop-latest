using Application.Common.Interfaces;
using Domain.Abstractions.Interfaces;
using Domain.Primitives;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Infrastructure.Interceptors;
public sealed class UpdateAuditableEntitiesInterceptor
    : SaveChangesInterceptor
{
    private readonly IDateTimeProvider dateTimeProvider;
    private readonly ICurrentUserService currentUserService;

    public UpdateAuditableEntitiesInterceptor(IDateTimeProvider dateTimeProvider, ICurrentUserService currentUserService)
    {
        this.dateTimeProvider = dateTimeProvider;
        this.currentUserService = currentUserService;
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

        IEnumerable<EntityEntry<IAuditableEntity>> entries =
            dbContext
                .ChangeTracker
                .Entries<IAuditableEntity>();

        foreach (EntityEntry<IAuditableEntity> entityEntry in entries)
        {
            if (entityEntry.State == EntityState.Added)
            {
                entityEntry.Property(a => a.CreatedOnUtc)
                    .CurrentValue = dateTimeProvider.UtcNow;

                entityEntry.Property(a => a.CreatedBy)
                    .CurrentValue = currentUserService.UserId;
            }

            if (entityEntry.State == EntityState.Added || entityEntry.State == EntityState.Modified)
            {
                entityEntry.Property(a => a.ModifiedOnUtc)
                    .CurrentValue = dateTimeProvider.UtcNow;

                entityEntry.Property(a => a.ModifiedBy)
                    .CurrentValue = currentUserService.UserId;
            }
        }

        return base.SavingChangesAsync(
            eventData,
            result,
            cancellationToken);
    }
}
