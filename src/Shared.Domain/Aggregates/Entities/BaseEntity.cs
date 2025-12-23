#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

namespace Shared.Domain.Aggregates.Entities;

public abstract class BaseEntity<TId>
{
    public TId Id { get; protected set; } = default!;
}

public abstract class BaseEntity<TId1, TId2>
{
    public TId1 Id1 { get; }
    public TId2 Id2 { get; }
}