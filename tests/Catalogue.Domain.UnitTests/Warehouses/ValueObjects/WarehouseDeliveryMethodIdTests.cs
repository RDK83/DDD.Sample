using Catalogue.Domain.UnitTests.Common.Abstract;
using Catalogue.Domain.Warehouses.ValueObjects;

namespace Catalogue.Domain.UnitTests.Warehouses.ValueObjects;

internal class WarehouseDeliveryMethodIdTests : IdentifierIntegerValueObjectTests<WarehouseDeliveryMethodId>
{
    protected override int ValidInput => 17;

    public override void ImplicitOperatorInteger_ReturnsValue()
    {
        var vo = CreateValueObjectWithValidInput();

        int value = vo;

        Assert.That(value, Is.EqualTo(ValidOutput));
    }
}