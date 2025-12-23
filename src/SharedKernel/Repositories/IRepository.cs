namespace SharedKernel.Repositories;

public interface IRepository<TEntity> : IReadRepository<TEntity>
{
    void Add(TEntity entity);
    void Remove(TEntity entity);
}