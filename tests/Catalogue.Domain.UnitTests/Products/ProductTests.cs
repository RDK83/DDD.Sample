using Catalogue.Domain.Common.Enums;
using Catalogue.Domain.Lookups.ManufacturerClassifications.ValueObjects;
using Catalogue.Domain.Merchants.ValueObjects;
using Catalogue.Domain.Products;
using Catalogue.Domain.Products.ValueObjects;
using Catalogue.Domain.ProductTypes.ValueObjects;
using Catalogue.Domain.UnitTests.Common;
using Catalogue.Domain.WarehouseMerchants.Policies;
using Shared.Domain.Exceptions;

namespace Catalogue.Domain.UnitTests.Products;

public class ProductTests
{
    [SetUp]
    public void SetUp()
    {
    }

    [Test]
    public void Create_WithValidInput_ShouldCreateSuccessfully()
    {
        var expectedProductCode = ProductTestHelper.ValidProductCode();
        var expectedProductTitle = ProductTitle.Create("ProductTitle");
        var expectedShortDescription = ProductDescription.Create("ShortDesc");
        var expectedTaxClass = TaxClass.Rated;
        var expectedProductTypeId = ProductTypeId.Create(1);
        var expectedProductSubTypeId = ProductSubTypeId.Create(1);
        var expectedManufacturerClassificationId = ManufacturerClassificationId.Create(1);

        var product = Product.Create(
            id: expectedProductCode,
            productTitle: expectedProductTitle,
            description: expectedShortDescription,
            taxClass: expectedTaxClass,
            productTypeId: expectedProductTypeId,
            productSubTypeId: expectedProductSubTypeId,
            manufacturerClassificationId: expectedManufacturerClassificationId
        );

        Assert.Multiple(() =>
        {
            Assert.That(product, Is.Not.Null);
            Assert.That(product.Id, Is.EqualTo(expectedProductCode));
            Assert.That(product.ProductTitle, Is.EqualTo(expectedProductTitle));
            Assert.That(product.Description, Is.EqualTo(expectedShortDescription));

            Assert.That(product.TaxClass, Is.EqualTo(expectedTaxClass));

            Assert.That(product.ProductTypeId, Is.EqualTo(expectedProductTypeId));
            Assert.That(product.ProductSubTypeId, Is.EqualTo(expectedProductSubTypeId));
            Assert.That(product.ManufacturerClassificationId, Is.EqualTo(expectedManufacturerClassificationId));
        });
    }

    [Test]
    public void UpdateDetails_WithValidInput_ShouldUpdateSuccessfully()
    {
        var product = ProductTestHelper.CreateBaseProduct();

        var mutation = ProductTestHelper.CreateEditProductMutation();

        product.UpdateDetails(mutation);

        Assert.Multiple(() =>
        {
            Assert.That(product.TaxClass, Is.EqualTo(mutation.TaxClass));

            Assert.That(product.ProductTypeId, Is.EqualTo(mutation.ProductTypeId));
            Assert.That(product.ProductSubTypeId, Is.EqualTo(mutation.ProductSubTypeId));
            Assert.That(product.ProductTitle, Is.EqualTo(mutation.ProductTitle));
            Assert.That(product.Description, Is.EqualTo(mutation.Description));
            Assert.That(product.ManufacturerClassificationId, Is.EqualTo(mutation.ManufacturerClassificationId));
        });
    }

    [Test]
    public void AddOffer_WithValidInput_ShouldAddOffer()
    {
        var product = ProductTestHelper.CreateBaseProduct();

        var newOfferValues = ProductTestHelper.CreateNewOfferMutation(product);

        product.AddOffer(newOfferValues);

        Assert.Multiple(() =>
        {
            Assert.That(product.Offers.Count, Is.EqualTo(1));
            Assert.That(product.Offers.First().ProductCode, Is.EqualTo(ProductTestHelper.ValidProductCode()));
        });
    }

    [Test]
    public void AddOffer_WithDuplicateInput_ShouldThrowDomainValidationException()
    {
        var product = ProductTestHelper.CreateBaseProduct();

        var newOfferMutation = ProductTestHelper.CreateNewOfferMutation(product);

        product.AddOffer(newOfferMutation);

        Assert.Throws<ChildEntityAlreadyExistsException>(() => product.AddOffer(newOfferMutation));
    }

    [Test]
    public void UpdateOffer_WithValidInput_ShouldUpdateOffer()
    {
        var product = ProductTestHelper.CreateBaseProduct();

        var newOfferMutation = ProductTestHelper.CreateNewOfferMutation(product);
        product.AddOffer(newOfferMutation);

        var offer = product.Offers.First();
        var fakeOfferId = OfferId.Create(123);
        TestReflectionHelper.SetAutoPropertyBackingField(offer, nameof(offer.Id), fakeOfferId);

        var updateOfferMutation = ProductTestHelper.CreateEditOfferMutation(product);

        product.UpdateOffer(fakeOfferId, updateOfferMutation);

        Assert.Multiple(() =>
        {
            Assert.That(product.Offers.Count, Is.EqualTo(1));
            Assert.That(product.Offers.First().GrossPrice, Is.EqualTo(updateOfferMutation.GrossPrice));
            Assert.That(product.Offers.First().StockLevel, Is.EqualTo(updateOfferMutation.StockLevel));
        });
    }

    [Test]
    public void UpdateOffer_WithNonExistentOfferId_ShouldThrow()
    {
        var product = ProductTestHelper.CreateBaseProduct();

        var mutation = ProductTestHelper.CreateNewOfferMutation(product);
        product.AddOffer(mutation);

        var offer = product.Offers.First();
        var fakeOfferId = OfferId.Create(123);
        TestReflectionHelper.SetAutoPropertyBackingField(offer, nameof(offer.Id), fakeOfferId);

        var updateOfferMutation = ProductTestHelper.CreateEditOfferMutation(product);

        var nonExistentOfferId = OfferId.Create(999);

        Assert.Throws<ChildEntityNotFoundException>(() => product.UpdateOffer(nonExistentOfferId, updateOfferMutation));
    }


    [Test]
    public void SetPromotedOffer_Sets_PromotedOfferId()
    {
        var product = ProductTestHelper.CreateBaseProduct();

        var newOfferValues1 = ProductTestHelper.CreateNewOfferMutation(product);
        var newOfferValues2 = ProductTestHelper.CreateNewOfferMutation(product, 2);

        product.AddOffer(newOfferValues1);
        product.AddOffer(newOfferValues2);

        var context = ProductTestHelper.CreateViableOfferCalculationContext();

        var offer1 = product.Offers.First();
        var fakeOfferId1 = OfferId.Create(123);
        TestReflectionHelper.SetAutoPropertyBackingField(offer1, nameof(offer1.Id), fakeOfferId1);

        var offer2 = product.Offers.Last();
        var fakeOfferId2 = OfferId.Create(456);
        TestReflectionHelper.SetAutoPropertyBackingField(offer2, nameof(offer2.Id), fakeOfferId2);

        var policy = new PreferredWarehouseMerchantPolicy();

        product.SetPromotedOffer(context, policy);

        Assert.That(product.PromotedOfferId.Value, Is.EqualTo(offer1.Id.Value));
    }

    [Test]
    public void GetOfferMerchants_WhenMultipleOffers_ReturnsCorrectOfferMerchants()
    {
        var product = ProductTestHelper.CreateBaseProduct();

        var newOfferValues1 = ProductTestHelper.CreateNewOfferMutation(product);
        var newOfferValues2 = ProductTestHelper.CreateNewOfferMutation(product, 2, 2);

        product.AddOffer(newOfferValues1);
        product.AddOffer(newOfferValues2);

        var offerMerchants = product.GetOfferMerchants().ToList();

        Assert.Multiple(() =>
        {
            Assert.That(offerMerchants.Count, Is.EqualTo(2));
            Assert.That(offerMerchants.First(), Is.EqualTo(MerchantId.Create(1)));
            Assert.That(offerMerchants.Last(), Is.EqualTo(MerchantId.Create(2)));
        });
    }

    [Test]
    public void GetOfferMerchants_WhenMultipleOffersWithDuplicateMerchants_ReturnsCorrectOfferMerchants()
    {
        var product = ProductTestHelper.CreateBaseProduct();

        var newOfferValues1 = ProductTestHelper.CreateNewOfferMutation(product);
        var newOfferValues2 = ProductTestHelper.CreateNewOfferMutation(product, 2);
        var newOfferValues3 = ProductTestHelper.CreateNewOfferMutation(product, 2, 2);

        product.AddOffer(newOfferValues1);
        product.AddOffer(newOfferValues2);
        product.AddOffer(newOfferValues3);

        var offerMerchants = product.GetOfferMerchants().ToList();

        Assert.Multiple(() =>
        {
            Assert.That(offerMerchants.Count, Is.EqualTo(2));
            Assert.That(offerMerchants.First(), Is.EqualTo(MerchantId.Create(1)));
            Assert.That(offerMerchants.Last(), Is.EqualTo(MerchantId.Create(2)));
        });
    }
}