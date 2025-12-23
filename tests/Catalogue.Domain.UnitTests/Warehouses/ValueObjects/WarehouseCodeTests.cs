using Catalogue.Domain.UnitTests.Common.Abstract;
using Catalogue.Domain.Warehouses.ValueObjects;

namespace Catalogue.Domain.UnitTests.Warehouses.ValueObjects;

internal class WarehouseCodeTests : NonNullableStringToUpperValueObjectTests<WarehouseCode>
{
    protected override string ValidInput => "winp";
    protected override string ValidOutput => "WINP";

    public override void ImplicitOperatorString_ReturnsValue()
    {
        var vo = CreateValueObjectWithValidInput();

        string value = vo;

        Assert.That(value, Is.EqualTo(ValidOutput));
    }
}