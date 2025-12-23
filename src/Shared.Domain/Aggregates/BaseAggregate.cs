using Shared.Domain.Aggregates.Entities;
using Shared.Domain.Events;

namespace Shared.Domain.Aggregates;

public abstract class BaseAggregate<TId> : BaseEntity<TId>, IAggregateRoot
{
    private List<BaseDomainEvent> _events { get; } = [];

    public IReadOnlyCollection<BaseDomainEvent> Events => _events.AsReadOnly();

    public void AddDomainEvent(BaseDomainEvent domainEvent)
    {
        _events.Add(domainEvent);
    }

    public void ClearDomainEvents()
    {
        _events.Clear();
    }
}