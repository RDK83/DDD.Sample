using Catalogue.Domain.Common.Enums;
using Catalogue.Domain.Common.ValueObjects.Address;
using Shared.Domain.Exceptions;

namespace Catalogue.Domain.UnitTests.Common.ValueObjects;

internal class PostalCodeTests
{
    private const string ValidPostcodeInput = "cw1 6ng";
    private const string ValidPostcodeOutput = "CW1 6NG";

    [Test]
    public void Create_WithValidInput_ShouldCreateSuccessfully()
    {
        var countryId = Country.GB;
        var postalCode = PostalCode.Create(ValidPostcodeInput, countryId);

        Assert.Multiple(() =>
        {
            Assert.That(postalCode.CountryId, Is.EqualTo(countryId));
            Assert.That(postalCode.Postcode, Is.EqualTo(ValidPostcodeOutput));
        });
    }

    [Test]
    public void Create_WithPostcodeTooLong_ShouldThrowDomainValidationException()
    {
        var tooLongValue = new string('X', 51);
        var countryId = Country.GB;

        Assert.Throws<DomainValidationException>(() => PostalCode.Create(tooLongValue, countryId));
    }

    [Test]
    public void Create_WithNullInput_ShouldThrowArgumentNullException()
    {
        var countryId = Country.GB;
        Assert.Throws<DomainValidationException>(() => PostalCode.Create(null!, countryId));
    }
}