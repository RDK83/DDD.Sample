namespace Shared.Domain.Lookups;

public abstract record LookupObject<TId>
{
    public TId Id { get; protected set; } = default!;
}