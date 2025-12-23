namespace Shared.Domain.Aggregates.ValueObjects;

public interface IValueObject<out T, TValue> where T : IValueObject<T, TValue>
{
    static abstract T Create(TValue input);

    TValue Value { get; }
}

public interface IValueObject<out T, in TValue1, in TValue2> where T : IValueObject<T, TValue1, TValue2>
{
    static abstract T Create(TValue1 input1, TValue2 input2);
}

public interface IValueObject<out T, in TValue1, in TValue2, in TValue3>
    where T : IValueObject<T, TValue1, TValue2, TValue3>
{
    static abstract T Create(TValue1 input1, TValue2 input2, TValue3 input3);
}

public interface IValueObject;