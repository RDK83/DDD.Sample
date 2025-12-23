using Shared.Domain.Aggregates.ValueObjects;

namespace Catalogue.Domain.UnitTests.Common.Abstract;

internal abstract class NullableIntegerValueObjectTests<T> : BaseSinglePropertyValueObjectTests<T, int?> where T : INullableIntegerValueObject<T>
{
    [Test]
    public void Create_WithNullValue_ShouldCreateSuccessfully()
    {
        var result = T.Create(null);

        Assert.That(result.Value, Is.Null);
    }

    [Test]
    public abstract void ImplicitOperatorInteger_ReturnsValue();
}