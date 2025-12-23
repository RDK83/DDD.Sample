namespace Shared.Domain.Aggregates.ValueObjects;

public interface INullableStringValueObject<out T> : IValueObject<T, string?> where T : IValueObject<T, string?>
{
    static abstract int MaxLength { get; }
}