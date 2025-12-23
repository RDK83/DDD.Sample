namespace Shared.Domain.Aggregates.ValueObjects;

public interface IByteValueObject<out T> : IValueObject<T, byte> where T : IByteValueObject<T>
{
    //static abstract T Create(byte input);
    //byte Value { get; }
}