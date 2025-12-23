using Mediator;

namespace Shared.Domain.Events;

public abstract record BaseDomainEvent : INotification
{
    public DateTimeOffset DateOccurred { get; init; } = DateTimeOffset.UtcNow;
}