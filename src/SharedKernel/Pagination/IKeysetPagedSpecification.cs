using System.Linq.Expressions;
using Ardalis.Specification;

namespace SharedKernel.Pagination;

public interface IKeysetPagedSpecification<TEntity, TKey> : ISpecification<TEntity>
{
    Expression<Func<TEntity, TKey>> KeySelector { get; }
}