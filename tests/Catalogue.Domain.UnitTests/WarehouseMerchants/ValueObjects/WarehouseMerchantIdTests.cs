using Catalogue.Domain.UnitTests.Common.Abstract;
using Catalogue.Domain.WarehouseMerchants.ValueObjects;

namespace Catalogue.Domain.UnitTests.WarehouseMerchants.ValueObjects;

internal class WarehouseMerchantIdTests : IdentifierIntegerValueObjectTests<WarehouseMerchantId>
{
    protected override int ValidInput => 42;

    public override void ImplicitOperatorInteger_ReturnsValue()
    {
        var vo = WarehouseMerchantId.Create(ValidInput);

        int value = vo;

        Assert.That(value, Is.EqualTo(ValidOutput));
    }
}