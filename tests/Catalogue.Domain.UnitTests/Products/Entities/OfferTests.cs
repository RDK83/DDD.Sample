using Catalogue.Domain.Merchants.ValueObjects;
using Catalogue.Domain.Products.ValueObjects;
using Catalogue.Domain.WarehouseMerchants.ValueObjects;
using Catalogue.Domain.Warehouses.ValueObjects;

namespace Catalogue.Domain.UnitTests.Products.Entities;

internal class OfferTests
{
    [Test]
    public void Create_WithValidInputs_ShouldCreateSuccessfully()
    {
        var offer = ProductTestHelper.CreateBaseOffer();

        Assert.Multiple(() => { Assert.That(offer, Is.Not.Null); });
    }


    [Test]
    public void IsViable_WhenNotForActiveMerchant_ReturnsFalse()
    {
        var context = ProductTestHelper.CreateViableOfferCalculationContext(
            merchants:
            [
                MerchantId.Create(3)
            ]);

        var offer = ProductTestHelper.CreateBaseOffer();

        var result = offer.IsViable(context);

        Assert.That(result, Is.False);
    }

    [Test]
    public void IsViable_WhenNotForAvailableWarehouse_ReturnsFalse()
    {
        var context = ProductTestHelper.CreateViableOfferCalculationContext(
            warehouses:
            [
                WarehouseId.Create(3)
            ]);

        var offer = ProductTestHelper.CreateBaseOffer();

        var result = offer.IsViable(context);

        Assert.That(result, Is.False);
    }

    [Test]
    public void IsViable_WhenNotForEligibleWarehouseMerchantPairing_ReturnsFalse()
    {
        var context = ProductTestHelper.CreateViableOfferCalculationContext(
            warehouseMerchants:
            [
                WarehouseMerchantStatus.Create(WarehouseId.Create(2), MerchantId.Create(1), PreferenceOrder.Create(10),
                    true)
            ]);

        var offer = ProductTestHelper.CreateBaseOffer();

        var result = offer.IsViable(context);

        Assert.That(result, Is.False);
    }

    [Test]
    public void IsViable_WhenOutOfStock_ReturnsFalse()
    {
        var context = ProductTestHelper.CreateViableOfferCalculationContext();

        var offer = ProductTestHelper.CreateBaseOffer(
            stockLevel: StockLevel.Create(0));

        var result = offer.IsViable(context);

        Assert.That(result, Is.False);
    }

    [Test]
    public void IsViable_WithValidData_ReturnsTrue()
    {
        var context = ProductTestHelper.CreateViableOfferCalculationContext();

        var offer = ProductTestHelper.CreateBaseOffer();

        var result = offer.IsViable(context);

        Assert.That(result, Is.True);
    }
}