using Catalogue.Domain.Merchants.ValueObjects;
using Catalogue.Domain.UnitTests.Common.Abstract;

namespace Catalogue.Domain.UnitTests.Merchants.ValueObjects;

internal class MerchantIdTests : IdentifierIntegerValueObjectTests<MerchantId>
{
    protected override int ValidInput => 1;

    public override void ImplicitOperatorInteger_ReturnsValue()
    {
        var vo = CreateValueObjectWithValidInput();

        int value = vo;

        Assert.That(value, Is.EqualTo(ValidOutput));
    }
}