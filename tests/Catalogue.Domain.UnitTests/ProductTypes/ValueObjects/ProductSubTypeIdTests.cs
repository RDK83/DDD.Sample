using Catalogue.Domain.ProductTypes.ValueObjects;
using Catalogue.Domain.UnitTests.Common.Abstract;

namespace Catalogue.Domain.UnitTests.ProductTypes.ValueObjects;

internal class ProductSubTypeIdTests : IdentifierIntegerValueObjectTests<ProductSubTypeId>
{
    protected override int ValidInput => 1;

    public override void ImplicitOperatorInteger_ReturnsValue()
    {
        var vo = CreateValueObjectWithValidInput();

        int value = vo;

        Assert.That(value, Is.EqualTo(ValidInput));
    }
}