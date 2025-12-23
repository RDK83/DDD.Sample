using Ardalis.Specification;
using Ardalis.Specification.EntityFrameworkCore;
using Catalogue.Infrastructure.Exceptions;
using Microsoft.EntityFrameworkCore;
using Shared.Domain.Aggregates.Entities;
using SharedKernel.Pagination;
using SharedKernel.Repositories;

namespace Catalogue.Infrastructure.Persistence.Repositories.Abstract;

public abstract class Repository<TEntity, TId> : IRepository<TEntity> where TEntity : BaseEntity<TId>
{
    protected readonly ApplicationDbContext ApplicationDbContext;

    protected Repository(ApplicationDbContext applicationDbContext)
    {
        ApplicationDbContext = applicationDbContext;
    }

    public virtual async Task<TEntity> GetFirstBySpecificationAsync(ISpecification<TEntity> specification)
    {
        // Ardalis specs can have no criteria (e.g. for include-only specs),
        // so remove this restriction or adapt as needed
        var query = SpecificationEvaluator.Default.GetQuery(ApplicationDbContext.Set<TEntity>(), specification);

        var entity = await query.FirstOrDefaultAsync();
        if (entity is null)
            throw new EntityNotFoundException(typeof(TEntity).Name);
        return entity;
    }

    public virtual async Task<List<TEntity>> GetManyBySpecificationAsync(ISpecification<TEntity> specification)
    {
        var query = SpecificationEvaluator.Default.GetQuery(ApplicationDbContext.Set<TEntity>(), specification);

        var entities = await query.ToListAsync();
        if (entities is null || entities.Count == 0)
            throw new EntityNotFoundException(typeof(TEntity).Name);
        return entities;
    }

    public virtual async Task<OffsetPagedResult<TEntity>> GetOffsetPagedBySpecificationAsync(
        ISpecification<TEntity> specification,
        int pageNumber,
        int pageSize)
    {
        var query = SpecificationEvaluator.Default.GetQuery(ApplicationDbContext.Set<TEntity>(), specification);

        var totalCount = await query.CountAsync();

        var items = await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return new OffsetPagedResult<TEntity>(items, totalCount, pageNumber, pageSize);
    }

    public virtual void Add(TEntity entity)
    {
        ApplicationDbContext.Set<TEntity>().Add(entity);
    }

    public virtual void Remove(TEntity entity)
    {
        ApplicationDbContext.Remove(entity);
    }

    public virtual async Task VerifyExistsAsync(ISpecification<TEntity> specification)
    {
        var query = SpecificationEvaluator.Default.GetQuery(ApplicationDbContext.Set<TEntity>(), specification);

        var entity = await query.FirstOrDefaultAsync();
        if (entity is null)
            throw new EntityNotFoundException(typeof(TEntity).Name);
    }
}