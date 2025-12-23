using Catalogue.Domain.UnitTests.Common.Abstract;
using Catalogue.Domain.Warehouses.ValueObjects;

namespace Catalogue.Domain.UnitTests.Warehouses.ValueObjects;

internal class WarehouseNameTests : NonNullableStringValueObjectTests<WarehouseName>
{
    protected override string ValidInput => "Warehouse PreOrder";

    public override void ImplicitOperatorString_ReturnsValue()
    {
        var vo = CreateValueObjectWithValidInput();

        string value = vo;

        Assert.That(value, Is.EqualTo(ValidOutput));
    }
}