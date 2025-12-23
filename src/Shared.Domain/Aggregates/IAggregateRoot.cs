using Shared.Domain.Events;

namespace Shared.Domain.Aggregates;

public interface IAggregateRoot
{
    IReadOnlyCollection<BaseDomainEvent> Events { get; }

    void AddDomainEvent(BaseDomainEvent domainEvent);

    void ClearDomainEvents();
}