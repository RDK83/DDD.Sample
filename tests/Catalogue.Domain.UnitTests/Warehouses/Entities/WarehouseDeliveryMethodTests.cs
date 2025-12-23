using Catalogue.Domain.Common.ValueObjects;
using Catalogue.Domain.Warehouses.Entities;
using Catalogue.Domain.Warehouses.ValueObjects;

namespace Catalogue.Domain.UnitTests.Warehouses.Entities;

internal class WarehouseDeliveryMethodTests
{
    [Test]
    public void Create_WithValidValues_ShouldCreateSuccessfully()
    {
        var entity = WarehouseDeliveryMethod.Create(
            WarehouseTestHelper.ValidWarehouseId(),
            WarehouseTestHelper.ValidDeliveryMethodId(),
            WarehouseTestHelper.ValidCountryCode(),
            Money.Create(20m),
            WarehouseDeliveryLeadTime.Create(1, 2),
            active: true
        );

        Assert.Multiple(() =>
        {
            Assert.That(entity, Is.Not.Null);
            Assert.That(entity.WarehouseId, Is.EqualTo(WarehouseTestHelper.ValidWarehouseId()));
            Assert.That(entity.DeliveryMethodId, Is.EqualTo(WarehouseTestHelper.ValidDeliveryMethodId()));
            Assert.That(entity.CountryIsoCode, Is.EqualTo(WarehouseTestHelper.ValidCountryCode()));
            Assert.That(entity.GrossCost, Is.EqualTo(Money.Create(20m)));
            Assert.That(entity.LeadTimes, Is.EqualTo(WarehouseDeliveryLeadTime.Create(1, 2)));
            Assert.That(entity.Active, Is.True);
        });
    }
}