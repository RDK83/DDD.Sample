using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Shared.Domain.Aggregates.Entities;

namespace Catalogue.Infrastructure.Persistence.Interceptors;

public class UpdateTimestampsInterceptor : SaveChangesInterceptor
{
    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
    {
        var context = eventData.Context;
        if (context is null) return new ValueTask<InterceptionResult<int>>(result);

        var now = DateTime.UtcNow;

        var entries = context.ChangeTracker.Entries()
            .Where(e => e.Entity is IHasUpdatedTimeStamp &&
                        (e.State == EntityState.Modified || e.State == EntityState.Added));

        foreach (var entry in entries)
        {
            ((IHasUpdatedTimeStamp)entry.Entity).UpdatedAt = now;

            if (entry.State == EntityState.Added && entry.Entity is IHasCreatedTimeStamp created)
            {
                created.CreatedAt = now;
            }
        }

        return new ValueTask<InterceptionResult<int>>(result);
    }
}