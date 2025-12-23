using Shared.Domain.Aggregates.ValueObjects;
using Shared.Domain.Exceptions;

namespace Catalogue.Domain.UnitTests.Common.Abstract;

internal abstract class IdentifierIntegerValueObjectTests<T> : BaseSinglePropertyValueObjectTests<T, int> where T : IIntegerValueObject<T>
{
    [Test]
    public void Create_WithZeroValue_ShouldThrow()
    {
        Assert.Throws<DomainValidationException>(() => T.Create(0));
    }

    [Test]
    public abstract void ImplicitOperatorInteger_ReturnsValue();
}