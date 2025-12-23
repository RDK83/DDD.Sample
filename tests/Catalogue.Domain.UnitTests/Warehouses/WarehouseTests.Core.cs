using Catalogue.Domain.Warehouses;
using Catalogue.Domain.Warehouses.ValueObjects;

namespace Catalogue.Domain.UnitTests.Warehouses;

internal partial class WarehouseTests
{
    [Test]
    public void Create_WithValidInput_ShouldCreateSuccessfully()
    {
        var warehouse = Warehouse.Create(
            WarehouseName.Create("Birmingham PreOrder"),
            WarehouseTestHelper.ValidWarehouseCode(),
            WarehouseTestHelper.ValidAddress(),
            active: false
        );

        Assert.Multiple(() =>
        {
            Assert.That(warehouse, Is.Not.Null);
            Assert.That(warehouse.WarehouseName, Is.EqualTo(WarehouseName.Create("Birmingham PreOrder")));
            Assert.That(warehouse.WarehouseCode, Is.EqualTo(WarehouseTestHelper.ValidWarehouseCode()));
            Assert.That(warehouse.Active, Is.False);
            Assert.That(warehouse.Address.AddressLine1, Is.EqualTo(WarehouseTestHelper.ValidAddress().AddressLine1));
            Assert.That(warehouse.Address.AddressLine2, Is.EqualTo(WarehouseTestHelper.ValidAddress().AddressLine2));
            Assert.That(warehouse.Address.AddressLine3, Is.EqualTo(WarehouseTestHelper.ValidAddress().AddressLine3));
            Assert.That(warehouse.Address.AddressLine4, Is.EqualTo(WarehouseTestHelper.ValidAddress().AddressLine4));
            Assert.That(warehouse.Address.City, Is.EqualTo(WarehouseTestHelper.ValidAddress().City));
            Assert.That(warehouse.Address.County, Is.EqualTo(WarehouseTestHelper.ValidAddress().County));
            Assert.That(warehouse.Address.PostalCode, Is.EqualTo(WarehouseTestHelper.ValidAddress().PostalCode));
        });
    }
}