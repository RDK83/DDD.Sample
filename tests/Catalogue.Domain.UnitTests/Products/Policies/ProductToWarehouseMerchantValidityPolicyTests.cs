using Catalogue.Domain.Merchants.ValueObjects;
using Catalogue.Domain.Products.Policies;
using Catalogue.Domain.WarehouseMerchants.ValueObjects;
using Catalogue.Domain.Warehouses.ValueObjects;
using Shared.Domain.Exceptions;

namespace Catalogue.Domain.UnitTests.Products.Policies;

internal class ProductToWarehouseMerchantValidityPolicyTests
{
    private readonly IReadOnlyCollection<WarehouseMerchantStatus> _warehouseMerchants =
        [WarehouseMerchantStatus.Create(WarehouseId.Create(1), MerchantId.Create(1), PreferenceOrder.Create(10), true)];

    private readonly ProductToWarehouseMerchantValidityPolicy _sut = new();

    [Test]
    public void DetermineValidForProduct_WhenValidWarehouseMerchantsExist_ShouldReturnValidValues()
    {
        var product = ProductTestHelper.CreateBaseProduct();

        var mutation = ProductTestHelper.CreateNewOfferMutation(product);

        product.AddOffer(mutation);

        var result = _sut.DetermineValidForProduct(_warehouseMerchants, product);

        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count, Is.EqualTo(1));
        });
    }

    [Test]
    public void DetermineValidForProduct_WithNullProduct_ShouldThrow()
    {
        Assert.Throws<DomainValidationException>(() => _sut.DetermineValidForProduct(_warehouseMerchants, null!));
    }

    [Test]
    public void DetermineValidForProduct_WhenValidWarehouseMerchantsExistButProductHasNoMatchingOffers_ShouldReturnEmptyCollection()
    {
        var product = ProductTestHelper.CreateBaseProduct();

        var result = _sut.DetermineValidForProduct(_warehouseMerchants, product);

        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count, Is.EqualTo(0));
        });
    }
}