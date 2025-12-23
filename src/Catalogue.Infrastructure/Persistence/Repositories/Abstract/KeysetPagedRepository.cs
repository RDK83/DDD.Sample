using Ardalis.Specification.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Shared.Domain.Aggregates.Entities;
using SharedKernel.Pagination;
using SharedKernel.Repositories;

namespace Catalogue.Infrastructure.Persistence.Repositories.Abstract;

public abstract class KeysetPagedRepository<TEntity, TId, TKey>
    : Repository<TEntity, TId>, IKeysetPagedRepository<TEntity, TKey>
    where TEntity : BaseEntity<TId>
{
    protected KeysetPagedRepository(ApplicationDbContext dbContext)
        : base(dbContext)
    {
    }

    public async Task<KeysetPagedResult<TEntity, TKey>> GetKeysetPagedAsync(
        IKeysetPagedSpecification<TEntity, TKey> specification,
        int pageSize,
        CancellationToken cancellationToken = default)
    {
        var query = SpecificationEvaluator.Default.GetQuery(ApplicationDbContext.Set<TEntity>(), specification);

        var items = await query.Take(pageSize).ToListAsync(cancellationToken);

        var last = items.LastOrDefault();
        var nextKey = last != null
            ? specification.KeySelector.Compile()(last)
            : default;

        return new KeysetPagedResult<TEntity, TKey>(items, nextKey);
    }
}