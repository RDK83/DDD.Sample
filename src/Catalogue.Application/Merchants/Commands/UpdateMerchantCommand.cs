using Catalogue.Domain.Common.Enums;
using Catalogue.Domain.Common.ValueObjects.Address;
using Catalogue.Domain.Merchants.Mutations;
using Shared.Domain.Exceptions;
using Shared.Domain.Exceptions.Helper;

namespace Catalogue.Application.Merchants.Commands;

public record UpdateMerchantCommand(
    string MerchantName,
    int MerchantId,
    string AddressLine1,
    string CountryIsoCode,
    string City,
    string PostCode,
    bool Active,
    string? AddressLine2,
    string? AddressLine3,
    string? AddressLine4,
    string? County)
{
    public EditMerchantMutation ToMutation()
    {
        var address = AddressToEntity();
        var editMerchantMutation = new EditMerchantMutation(
            Domain.Merchants.ValueObjects.MerchantName.Create(MerchantName),
            address, Active);

        return editMerchantMutation;
    }

    private Address AddressToEntity()
    {
        if (!Enum.TryParse<Country>(CountryIsoCode, out var parsedCountry))
            throw new DomainValidationException(DomainErrorMessages.CountryParsingFailed);

        var address = Address.Create(
            PrimaryAddressLine.Create(AddressLine1),
            AddressLine.Create(AddressLine2),
            AddressLine.Create(AddressLine3),
            AddressLine.Create(AddressLine4),
            Domain.Common.ValueObjects.Address.City.Create(City),
            Domain.Common.ValueObjects.Address.County.Create(County),
            PostalCode.Create(PostCode, parsedCountry)
        );

        return address;
    }
}