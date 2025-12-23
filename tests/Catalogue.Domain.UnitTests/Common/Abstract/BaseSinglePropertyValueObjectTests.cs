using Shared.Domain.Aggregates.ValueObjects;

namespace Catalogue.Domain.UnitTests.Common.Abstract;

internal abstract class BaseSinglePropertyValueObjectTests<T, TValue> where T : IValueObject<T, TValue>
{
    protected abstract TValue ValidInput { get; }

    protected virtual TValue ValidOutput => ValidInput;

    protected T CreateValueObjectWithValidInput() => T.Create(ValidInput);

    [Test]
    public void Create_WithValidInput_ShouldCreateSuccessfully()
    {
        var vo = CreateValueObjectWithValidInput();
        Assert.That(vo.Value, Is.EqualTo(ValidOutput));
    }
}