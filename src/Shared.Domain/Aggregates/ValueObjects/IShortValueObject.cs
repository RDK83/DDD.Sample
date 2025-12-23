namespace Shared.Domain.Aggregates.ValueObjects;

public interface IShortValueObject<out T> : IValueObject<T, short> where T : IValueObject<T, short>
{
}