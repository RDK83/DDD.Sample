using Catalogue.Domain.Merchants.ValueObjects;
using Catalogue.Domain.WarehouseMerchants;
using Catalogue.Domain.WarehouseMerchants.Mutations;
using Catalogue.Domain.WarehouseMerchants.Policies;
using Catalogue.Domain.WarehouseMerchants.ValueObjects;
using Catalogue.Domain.Warehouses.ValueObjects;
using Shared.Domain.Exceptions;

namespace Catalogue.Domain.UnitTests.WarehouseMerchants;

public class WarehouseMerchantTests
{
    [Test]
    public void Create_ValidValues_CreatesCorrectly()
    {
        var uniquenessPolicy =
            new WarehouseMerchantUniquePreferenceOrderPolicy(new List<PreferenceOrder> { PreferenceOrder.Create(99) });
        var warehouseMerchant = WarehouseMerchant.Create(
            WarehouseId.Create(1),
            MerchantId.Create(1),
            PreferenceOrder.Create(1),
            true,
            uniquenessPolicy);

        Assert.Multiple(() =>
        {
            Assert.That(warehouseMerchant, Is.Not.Null);
            Assert.That(warehouseMerchant.WarehouseId.Value, Is.EqualTo(1));
            Assert.That(warehouseMerchant.MerchantId.Value, Is.EqualTo(1));
            Assert.That(warehouseMerchant.PreferenceOrder.Value, Is.EqualTo(1));
            Assert.That(warehouseMerchant.Active, Is.EqualTo(true));
        });
    }

    [Test]
    public void Create_WhenPreferenceOrderIsNotUnique_Throws()
    {
        var existingOrders = new List<PreferenceOrder> { PreferenceOrder.Create(1), PreferenceOrder.Create(2) };
        var uniquenessPolicy = new WarehouseMerchantUniquePreferenceOrderPolicy(existingOrders);

        Assert.Throws<DomainValidationException>(()
            => WarehouseMerchant.Create(WarehouseId.Create(1), MerchantId.Create(1), PreferenceOrder.Create(2), true,
                uniquenessPolicy));
    }


    [Test]
    public void Update_WithValidValues_UpdatesCorrectly()
    {
        var createUniquenessPolicy = new WarehouseMerchantUniquePreferenceOrderPolicy([]);
        var warehouseMerchant = WarehouseMerchant.Create(WarehouseId.Create(1), MerchantId.Create(1),
            PreferenceOrder.Create(1), true, createUniquenessPolicy);

        var editValues = new EditWarehouseMerchantMutation(PreferenceOrder.Create(2), !warehouseMerchant.Active);
        var updateUniquenessPolicy =
            new WarehouseMerchantUniquePreferenceOrderPolicy([warehouseMerchant.PreferenceOrder]);

        warehouseMerchant.Update(editValues, updateUniquenessPolicy);

        Assert.Multiple(() =>
        {
            Assert.That(warehouseMerchant.PreferenceOrder.Value, Is.EqualTo(2));
            Assert.That(warehouseMerchant.Active, Is.EqualTo(editValues.Active));
        });
    }

    [Test]
    public void Update_WhenPreferenceOrderIsNotUnique_Throws()
    {
        var createUniquenessPolicy = new WarehouseMerchantUniquePreferenceOrderPolicy([]);
        var warehouseMerchant = WarehouseMerchant.Create(WarehouseId.Create(1), MerchantId.Create(1),
            PreferenceOrder.Create(1), true, createUniquenessPolicy);

        var editValues = new EditWarehouseMerchantMutation(PreferenceOrder.Create(2), !warehouseMerchant.Active);
        var updateUniquenessPolicy =
            new WarehouseMerchantUniquePreferenceOrderPolicy([
                warehouseMerchant.PreferenceOrder, editValues.PreferenceOrder
            ]);

        var exception =
            Assert.Throws<DomainValidationException>(() =>
                warehouseMerchant.Update(editValues, updateUniquenessPolicy));

        Assert.That(exception.Message,
            Is.EqualTo("The preference order must be unique among existing warehouse merchants when Updating."));
    }

    [Test]
    public void Update_ActivateProperty_ShouldUpdate()
    {
        var createUniquenessPolicy = new WarehouseMerchantUniquePreferenceOrderPolicy([]);
        var warehouseMerchant = WarehouseMerchant.Create(WarehouseId.Create(1), MerchantId.Create(1),
            PreferenceOrder.Create(1), true, createUniquenessPolicy);

        var editValues =
            new EditWarehouseMerchantMutation(warehouseMerchant.PreferenceOrder, !warehouseMerchant.Active);
        var updateUniquenessPolicy =
            new WarehouseMerchantUniquePreferenceOrderPolicy([warehouseMerchant.PreferenceOrder]);
        warehouseMerchant.Update(editValues, updateUniquenessPolicy);

        Assert.Multiple(() =>
        {
            Assert.That(warehouseMerchant.PreferenceOrder.Value, Is.EqualTo(editValues.PreferenceOrder.Value));
            Assert.That(warehouseMerchant.Active, Is.EqualTo(editValues.Active));
        });
    }

    [Test]
    public void ToWarehouseMerchantStatus_ValidValues_ShouldCreate()
    {
        var uniquenessPolicy =
            new WarehouseMerchantUniquePreferenceOrderPolicy(new List<PreferenceOrder> { PreferenceOrder.Create(99) });
        var warehouseMerchant = WarehouseMerchant.Create(
            WarehouseId.Create(1),
            MerchantId.Create(1),
            PreferenceOrder.Create(1),
            true,
            uniquenessPolicy);

        var warehouseMerchantStatus = warehouseMerchant.ToWarehouseMerchantStatus();

        Assert.Multiple(() =>
        {
            Assert.That(warehouseMerchantStatus, Is.Not.Null);
            Assert.That(warehouseMerchantStatus.WarehouseId.Value, Is.EqualTo(1));
            Assert.That(warehouseMerchantStatus.MerchantId.Value, Is.EqualTo(1));
            Assert.That(warehouseMerchantStatus.PreferenceOrder.Value, Is.EqualTo(1));
            Assert.That(warehouseMerchantStatus.Active, Is.EqualTo(true));
        });
    }
}