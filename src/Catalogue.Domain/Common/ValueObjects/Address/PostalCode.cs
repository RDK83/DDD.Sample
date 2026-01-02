using Catalogue.Domain.Common.Enums;
using Shared.Domain.Aggregates.ValueObjects;
using Shared.Domain.Guard;
using ValidationRules;

namespace Catalogue.Domain.Common.ValueObjects.Address;

public record PostalCode : IValueObject<PostalCode, string, Country>
{
    public string Postcode { get; }
    public Country CountryId { get; }

    private PostalCode(string postcode, Country countryId)
    {
        Postcode = postcode;
        CountryId = countryId;
    }

    public static PostalCode Create(string postcode, Country countryId)
    {
        DomainGuard.AgainstNullOrWhiteSpace(postcode);
        DomainGuard.AgainstNull(countryId);

        postcode = postcode.Trim().ToUpperInvariant();

        DomainGuard.AgainstStringTooLong(postcode, AddressValidationRules.PostCodeMaxLength);

        return new PostalCode(postcode, countryId);
    }
}