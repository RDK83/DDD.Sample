using Catalogue.Domain.UnitTests.Common.Abstract;
using Catalogue.Domain.WarehouseMerchants.ValueObjects;

namespace Catalogue.Domain.UnitTests.WarehouseMerchants.ValueObjects;

internal class PreferenceOrderTests : NonNegativeIntegerValueObjectTests<PreferenceOrder>
{
    protected override int ValidInput => 1;

    public override void ImplicitOperatorInteger_ReturnsValue()
    {
        var vo = CreateValueObjectWithValidInput();

        int value = vo;

        Assert.That(value, Is.EqualTo(ValidOutput));
    }
}