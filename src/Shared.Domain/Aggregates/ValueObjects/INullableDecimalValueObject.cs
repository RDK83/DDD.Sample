namespace Shared.Domain.Aggregates.ValueObjects;

public interface INullableDecimalValueObject<out T> : IValueObject<T, decimal?> where T : IValueObject<T, decimal?>
{
}