using Shared.Domain.Aggregates.ValueObjects;

namespace Catalogue.Domain.UnitTests.Common.Abstract;

internal abstract class NullableDecimalValueObjectTests<T> : BaseSinglePropertyValueObjectTests<T, decimal?> where T : INullableDecimalValueObject<T>
{
    [Test]
    public void Create_WithNullInput_ShouldCreateSuccessfully()
    {
        var result = T.Create(null);

        Assert.That(result.Value, Is.Null);
    }

    [Test]
    public abstract void ImplicitOperatorDecimal_ReturnsValue();
}