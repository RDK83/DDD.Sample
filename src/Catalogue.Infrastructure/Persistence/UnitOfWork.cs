using Catalogue.Infrastructure.Exceptions;
using Mediator;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Shared.Application.UnitOfWork;
using Shared.Domain.Aggregates;
using Shared.Domain.Events;

namespace Catalogue.Infrastructure.Persistence;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _applicationDbContext;
    private readonly IMediator _mediator;
    private ILogger<UnitOfWork> _logger;

    public UnitOfWork(ApplicationDbContext applicationDbContext, IMediator mediator, ILogger<UnitOfWork> logger)
    {
        _applicationDbContext = applicationDbContext;
        _mediator = mediator;
        _logger = logger;
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        try
        {
            var aggregatesWithEvents = _applicationDbContext.ChangeTracker
                .Entries()
                .Select(e => e.Entity)
                .OfType<IAggregateRoot>()
                .Where(e => e.Events.Count > 0)
                .ToArray();

            var allEvents = aggregatesWithEvents
                .SelectMany(e => e.Events)
                .ToArray();

            foreach (var aggregate in aggregatesWithEvents)
                aggregate.ClearDomainEvents();

            // --- 1. Dispatch atomic events (before commit) ---
            var atomicEvents = allEvents.OfType<AtomicDomainEvent>().ToArray();
            foreach (var atomicEvent in atomicEvents)
                await _mediator.Publish(atomicEvent, cancellationToken);

            // --- 2. Commit transaction ---
            var result = await _applicationDbContext.SaveChangesAsync(cancellationToken);

            // --- 3. Dispatch post-commit events (after commit, in parallel, error-isolated) ---
            var postCommitEvents = allEvents.OfType<DeferredDomainEvent>().ToArray();

            if (postCommitEvents.Length > 0)
            {
                _logger.LogInformation("Dispatching {Count} deferred domain events after commit",
                    postCommitEvents.Length);

                var tasks = postCommitEvents.Select(async evt =>
                {
                    try
                    {
                        await _mediator.Publish(evt, cancellationToken);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Post-commit handler for {EventName} failed", evt.GetType().Name);
                    }
                });

                await Task.WhenAll(tasks);
            }

            return result;
        }
        catch (DbUpdateException e) when (IsForeignKeyViolation(e))
        {
            throw new ForeignKeyViolationException("Reference to a non-existent entity");
        }

        catch (DbUpdateException e) when (IsUniqueConstraintViolation(e))
        {
            throw new UniqueConstraintViolationException("Unique constraint violation");
        }

        catch (DbUpdateException e) when (IsDuplicateKeyViolation(e))
        {
            throw new DuplicateKeyViolationException("Duplicate key violation");
        }

        catch (Exception e)
        {
            _logger.LogError(e, "Unhandled exception occured during {Method}", nameof(SaveChangesAsync));
            throw;
        }
    }


    private static bool IsForeignKeyViolation(DbUpdateException ex)
    {
        return ex.InnerException is SqlException sqlEx &&
               sqlEx.Number == 547; // 547 = FK violation
    }

    private static bool IsUniqueConstraintViolation(DbUpdateException ex)
    {
        return ex.InnerException is SqlException sqlEx &&
               sqlEx.Number == 2627; // 2627 = Unique Constraint violation
    }

    private static bool IsDuplicateKeyViolation(DbUpdateException ex)
    {
        return ex.InnerException is SqlException sqlEx &&
               sqlEx.Number == 2601; // 2601 = Duplicate Key violation
    }
}