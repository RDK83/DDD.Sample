using Catalogue.Domain.Products.ValueObjects;
using Catalogue.Domain.UnitTests.Common.Abstract;

namespace Catalogue.Domain.UnitTests.Products.ValueObjects;

internal class ProductDescriptionTests : NonNullableStringValueObjectTests<ProductDescription>
{
    protected override string ValidInput => "Valid Input";

    [Test]
    public override void ImplicitOperatorString_ReturnsValue()
    {
        var vo = CreateValueObjectWithValidInput();

        string value = vo;

        Assert.That(value, Is.EqualTo(ValidOutput));
    }
}