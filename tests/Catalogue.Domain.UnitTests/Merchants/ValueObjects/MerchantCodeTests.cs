using Catalogue.Domain.Merchants.ValueObjects;
using Catalogue.Domain.UnitTests.Common.Abstract;

namespace Catalogue.Domain.UnitTests.Merchants.ValueObjects;

internal class MerchantCodeTests : NonNullableStringToUpperValueObjectTests<MerchantCode>
{
    protected override string ValidInput => "cpj";
    protected override string ValidOutput => "CPJ";

    [Test]
    public override void ImplicitOperatorString_ReturnsValue()
    {
        var vo = CreateValueObjectWithValidInput();

        string value = vo;

        Assert.That(value, Is.EqualTo(ValidOutput));
    }
}