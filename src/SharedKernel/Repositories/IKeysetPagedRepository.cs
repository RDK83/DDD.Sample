using SharedKernel.Pagination;

namespace SharedKernel.Repositories;

public interface IKeysetPagedRepository<TEntity, TKey>
{
    Task<KeysetPagedResult<TEntity, TKey>> GetKeysetPagedAsync(
        IKeysetPagedSpecification<TEntity, TKey> specification,
        int pageSize,
        CancellationToken cancellationToken = default);
}