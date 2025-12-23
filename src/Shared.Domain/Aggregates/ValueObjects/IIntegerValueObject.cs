namespace Shared.Domain.Aggregates.ValueObjects;

public interface IIntegerValueObject<out T> : IValueObject<T, int> where T : IValueObject<T, int>
{
}