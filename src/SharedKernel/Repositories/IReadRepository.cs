using Ardalis.Specification;
using SharedKernel.Pagination;

namespace SharedKernel.Repositories;

public interface IReadRepository<TEntity>
{
    Task VerifyExistsAsync(ISpecification<TEntity> specification);
    Task<TEntity> GetFirstBySpecificationAsync(ISpecification<TEntity> specification);
    Task<List<TEntity>> GetManyBySpecificationAsync(ISpecification<TEntity> specification);

    Task<OffsetPagedResult<TEntity>> GetOffsetPagedBySpecificationAsync(
        ISpecification<TEntity> specification,
        int pageNumber,
        int pageSize);
}