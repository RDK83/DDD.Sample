namespace Shared.Domain.Aggregates.ValueObjects;

public interface IDecimalValueObject<out T> : IValueObject<T, decimal> where T : IDecimalValueObject<T>
{
}