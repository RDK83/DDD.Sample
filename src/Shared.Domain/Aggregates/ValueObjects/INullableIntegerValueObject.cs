namespace Shared.Domain.Aggregates.ValueObjects;

public interface INullableIntegerValueObject<out T> : IValueObject<T, int?> where T : IValueObject<T, int?>
{
}