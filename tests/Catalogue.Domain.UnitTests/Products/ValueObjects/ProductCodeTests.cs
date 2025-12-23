using Catalogue.Domain.Products.ValueObjects;
using Catalogue.Domain.UnitTests.Common.Abstract;

namespace Catalogue.Domain.UnitTests.Products.ValueObjects;

internal class ProductCodeTests : NonNullableStringToUpperValueObjectTests<ProductCode>
{
    protected override string ValidInput => "Product123";
    protected override string ValidOutput => "PRODUCT123";

    [Test]
    public override void ImplicitOperatorString_ReturnsValue()
    {
        var vo = CreateValueObjectWithValidInput();

        string value = vo;

        Assert.That(value, Is.EqualTo(ValidOutput));
    }
}