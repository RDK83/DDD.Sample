using Catalogue.Domain.Merchants.ValueObjects;
using Catalogue.Domain.WarehouseMerchants.Policies;
using Catalogue.Domain.WarehouseMerchants.ValueObjects;
using Catalogue.Domain.Warehouses.ValueObjects;
using Shared.Domain.Exceptions;

namespace Catalogue.Domain.UnitTests.WarehouseMerchants.Policies;

public class WarehouseMerchantPreferencePolicyTests
{
    private PreferredWarehouseMerchantPolicy _sut;

    private readonly PreferenceOrder _lowestPreferenceOrder = PreferenceOrder.Create(1);
    private readonly PreferenceOrder _highestPreferenceOrder = PreferenceOrder.Create(99);


    [SetUp]
    public void SetUp()
    {
        _sut = new PreferredWarehouseMerchantPolicy();
    }

    [Test]
    public void GetPreferredWarehouseMerchants_WhenNullInput_ThrowsArgumentNullException()
    {
        Assert.Throws<DomainValidationException>(() => { _sut.DeterminePreferredWarehouseMerchants(null!); });
    }

    [Test]
    public void GetPreferredWarehouseMerchants_WhenEmptyInput_ReturnsEmptyCollection()
    {
        var result = _sut.DeterminePreferredWarehouseMerchants(Array.Empty<WarehouseMerchantStatus>());
        Assert.That(result, Is.Empty);
    }

    [Test]
    public void GetPreferredWarehouseMerchants_DuplicateInputs_RemovesDuplicatesKeepingLowestPreferenceOrder()
    {
        var merchantId = MerchantId.Create(1);
        var warehouseId = WarehouseId.Create(1);

        var status1 = WarehouseMerchantStatus.Create(warehouseId, merchantId, _highestPreferenceOrder, true);
        var status2 =
            WarehouseMerchantStatus.Create(warehouseId, merchantId, _lowestPreferenceOrder,
                true); // Lower PreferenceOrder should be kept

        var input = new[] { status1, status2 };

        var result = _sut.DeterminePreferredWarehouseMerchants(input);

        Assert.That(result.Count, Is.EqualTo(1));
        Assert.That(result.Single(), Is.EqualTo(status2));
    }

    [Test]
    public void GetPreferredWarehouseMerchants_MultipleUniqueMerchants_ReturnsAllUniqueMerchants()
    {
        var merchantId1 = MerchantId.Create(1);
        var merchantId2 = MerchantId.Create(2);
        var warehouseId = WarehouseId.Create(1);

        var status1 = WarehouseMerchantStatus.Create(warehouseId, merchantId1, _highestPreferenceOrder, true);
        var status2 = WarehouseMerchantStatus.Create(warehouseId, merchantId2, _highestPreferenceOrder, true);
        var input = new[] { status1, status2 };

        var result = _sut.DeterminePreferredWarehouseMerchants(input);

        Assert.That(result.Count, Is.EqualTo(2));
        Assert.That(result, Does.Contain(status1));
        Assert.That(result, Does.Contain(status2));
    }

    [Test]
    public void GetPromotedWarehouseMerchant_WhenNullInput_ThrowsArgumentNullException()
    {
        Assert.Throws<DomainValidationException>(() => { _sut.DetermineMostPreferredWarehouseMerchant(null!); });
    }

    [Test]
    public void GetPromotedWarehouseMerchant_WhenEmptyInput_ThrowsDomainValidationException()
    {
        Assert.Throws<PolicyException>(() =>
        {
            _sut.DetermineMostPreferredWarehouseMerchant(Array.Empty<WarehouseMerchantStatus>());
        });
    }

    [Test]
    public void GetPromotedWarehouseMerchant_ValidInput_ReturnsStatusWithLowestPreferenceOrder()
    {
        var warehouseId = WarehouseId.Create(1);
        var status1 = WarehouseMerchantStatus.Create(warehouseId, MerchantId.Create(1), _highestPreferenceOrder, true);
        var status2 = WarehouseMerchantStatus.Create(warehouseId, MerchantId.Create(2), _lowestPreferenceOrder, true);
        var status3 = WarehouseMerchantStatus.Create(warehouseId, MerchantId.Create(3), _highestPreferenceOrder, true);

        var input = new[] { status1, status2, status3 };

        var result = _sut.DetermineMostPreferredWarehouseMerchant(input);

        Assert.That(result, Is.EqualTo(status2));
    }
}