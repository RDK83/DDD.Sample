using Catalogue.Domain.ProductTypes.ValueObjects;
using Catalogue.Domain.UnitTests.Common.Abstract;

namespace Catalogue.Domain.UnitTests.ProductTypes.ValueObjects;

internal class ProductTypeIdTests : IdentifierIntegerValueObjectTests<ProductTypeId>
{
    protected override int ValidInput => 12;

    public override void ImplicitOperatorInteger_ReturnsValue()
    {
        var vo = CreateValueObjectWithValidInput();

        int value = vo;

        Assert.That(value, Is.EqualTo(ValidInput));
    }
}