using Catalogue.Domain.ProductTypes.ValueObjects;
using Catalogue.Domain.UnitTests.Common.Abstract;

namespace Catalogue.Domain.UnitTests.ProductTypes.ValueObjects;

internal class ProductTypeNameTests : NonNullableStringValueObjectTests<ProductTypeName>
{
    protected override string ValidInput => "Valid Input";

    public override void ImplicitOperatorString_ReturnsValue()
    {
        var vo = CreateValueObjectWithValidInput();

        string value = vo;

        Assert.That(value, Is.EqualTo(ValidOutput));
    }
}