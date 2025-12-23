using Catalogue.Domain.Products.ValueObjects;
using Catalogue.Domain.UnitTests.Common.Abstract;

namespace Catalogue.Domain.UnitTests.Products.ValueObjects;

internal class OfferIdTests : IdentifierIntegerValueObjectTests<OfferId>
{
    protected override int ValidInput => 123;

    public override void ImplicitOperatorInteger_ReturnsValue()
    {
        var vo = CreateValueObjectWithValidInput();

        int value = vo;

        Assert.That(value, Is.EqualTo(ValidOutput));
    }
}