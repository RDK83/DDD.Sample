using Catalogue.Domain.Common.Enums;
using Shared.Domain.Exceptions;

namespace Catalogue.Domain.UnitTests.Merchants;

public class MerchantTests
{
    [SetUp]
    public void SetUp()
    {
    }

    [Test]
    public void CreateMerchant_ValidDataIsUsed_MerchantCreated()
    {
        var merchant = MerchantTestHelper.CreateValidMerchant();
        Assert.That(merchant, Is.Not.Null);
    }

    [Test]
    public void CreateMerchant_MissingRequiredValue_DomainExceptionIsThrown()
    {
        Assert.Throws<DomainValidationException>(() => MerchantTestHelper.CreateInvalidMerchant());
    }

    [Test]
    public void UpdateDetails_ValidEditMutationIsUsed_MerchantValuesAreUpdatedSuccessfully()
    {
        var existingMerchant = MerchantTestHelper.CreateValidMerchant();
        var mutation = MerchantTestHelper.CreateValidEditMerchantMutation();
        existingMerchant.UpdateDetails(mutation);

        Assert.Multiple(() =>
        {
            Assert.That(existingMerchant.MerchantCode.Value, Is.EqualTo("TVM"));
            Assert.That(existingMerchant.MerchantName.Value, Is.EqualTo("Test Valid Merchant Altered"));
            Assert.That(existingMerchant.Active, Is.False);

            Assert.That(existingMerchant.Address.AddressLine1.Value, Is.EqualTo("Road One"));
            Assert.That(existingMerchant.Address.AddressLine2.Value, Is.EqualTo("Birmingham Industrial Estate"));
            Assert.That(existingMerchant.Address.AddressLine3.Value, Is.EqualTo("Turtle Way"));
            Assert.That(existingMerchant.Address.AddressLine4.Value, Is.EqualTo("Sample Address Line 4"));
            Assert.That(existingMerchant.Address.City.Value, Is.EqualTo("Birmingham"));
            Assert.That(existingMerchant.Address.County.Value, Is.EqualTo("West Midlands Still"));
            Assert.That(existingMerchant.Address.PostalCode.Postcode, Is.EqualTo("B7 3QA"));
            Assert.That(existingMerchant.Address.PostalCode.CountryId, Is.EqualTo(Country.GB));
        });
    }

    [Test]
    public void UpdateDetails_InvalidEditMutationIsUsed_DomainExceptionIsThrown()
    {
        var existingMerchant = MerchantTestHelper.CreateValidMerchant();
        Assert.Throws<DomainValidationException>(() => existingMerchant.UpdateDetails(null!));
    }
}