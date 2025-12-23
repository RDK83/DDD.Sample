using Catalogue.Domain.Merchants.ValueObjects;
using Catalogue.Domain.UnitTests.Common.Abstract;

namespace Catalogue.Domain.UnitTests.Merchants.ValueObjects;

internal class MerchantNameTests : NonNullableStringValueObjectTests<MerchantName>
{
    protected override string ValidInput => "Merchant One";

    [Test]
    public override void ImplicitOperatorString_ReturnsValue()
    {
        var vo = CreateValueObjectWithValidInput();

        string value = vo;

        Assert.That(value, Is.EqualTo(ValidOutput));
    }
}