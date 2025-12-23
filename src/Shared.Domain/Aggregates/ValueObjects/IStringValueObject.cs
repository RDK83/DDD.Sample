namespace Shared.Domain.Aggregates.ValueObjects;

public interface IStringValueObject<out T> : IValueObject<T, string> where T : IValueObject<T, string>
{
    static abstract int MaxLength { get; }
}