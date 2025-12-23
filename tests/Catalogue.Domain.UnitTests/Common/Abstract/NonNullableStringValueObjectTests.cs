using Shared.Domain.Aggregates.ValueObjects;
using Shared.Domain.Exceptions;

namespace Catalogue.Domain.UnitTests.Common.Abstract;

internal abstract class NonNullableStringValueObjectTests<T> : BaseSinglePropertyValueObjectTests<T, string> where T : IStringValueObject<T>
{
    protected virtual bool AllowEmpty => false;
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
    public void Create_WithNull_ShouldThrow()
    {
        Assert.Throws<DomainValidationException>(() => T.Create(null!));
    }

    [Test]
    public void Create_WithEmptyString_ShouldBehaveAsExpected()
    {
        if (AllowEmpty)
        {
            var vo = T.Create("");
            Assert.That(vo.Value, Is.EqualTo(""));
        }
        else
        {
            Assert.Throws<DomainValidationException>(() => T.Create(string.Empty));
        }
    }

    [Test]
    public abstract void ImplicitOperatorString_ReturnsValue();
}