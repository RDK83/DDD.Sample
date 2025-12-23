using Shared.Domain.Aggregates.ValueObjects;
using Shared.Domain.Exceptions;

namespace Catalogue.Domain.UnitTests.Common.Abstract;

internal abstract class NullableStringValueObjectTests<T> : BaseSinglePropertyValueObjectTests<T, string?> where T : INullableStringValueObject<T>
{
    private string ValidInputWithWhiteSpace => $" {ValidInput} ";


    [Test]
    public void Create_ValidInputWithWhiteSpace_ShouldTrimAndCreateSuccessfully()
    {
        var vo = T.Create(ValidInputWithWhiteSpace);
        Assert.That(vo.Value, Is.EqualTo(ValidOutput));
    }

    [Test]
    public void Create_InputTooLong_ShouldThrow()
    {
        var tooLong = new string('x', T.MaxLength + 1);
        Assert.Throws<DomainValidationException>(() => T.Create(tooLong));
    }

    [Test]
    public void Create_WithNull_ShouldCreateWithNullValue()
    {
        var vo = T.Create(null);
        Assert.That(vo.Value, Is.Null);
    }

    [Test]
    public void Create_WithEmptyString_ShouldCreateWithEmptyValue()
    {
        var vo = T.Create("");
        Assert.That(vo.Value, Is.EqualTo(string.Empty));
    }


    [Test]
    public abstract void ImplicitOperatorString_ReturnsValue();
}