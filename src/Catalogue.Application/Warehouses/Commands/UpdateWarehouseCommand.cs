using Catalogue.Domain.Common.Enums;
using Catalogue.Domain.Common.ValueObjects.Address;
using Catalogue.Domain.Warehouses.Mutations;
using Shared.Domain.Exceptions;
using Shared.Domain.Exceptions.Helper;

namespace Catalogue.Application.Warehouses.Commands;

public record UpdateWarehouseCommand(
    int WarehouseId,
    string WarehouseName,
    string AddressLine1,
    string CountryIsoCode,
    string City,
    string PostCode,
    bool Active,
    string? AddressLine2,
    string? AddressLine3,
    string? AddressLine4,
    string? County
)
{
    public EditWarehouseMutation ToMutation()
    {
        var address = AddressToEntity();
        var editMerchantMutation = new EditWarehouseMutation(
            WarehouseName: Domain.Warehouses.ValueObjects.WarehouseName.Create(WarehouseName),
            Address: address,
            Active: Active
        );

        return editMerchantMutation;
    }

    private Address AddressToEntity()
    {
        if (!Enum.TryParse<Country>(CountryIsoCode, out var parsedCountry))
            throw new DomainValidationException(DomainErrorMessages.CountryParsingFailed);

        var address = Address.Create(
            addressLine1: PrimaryAddressLine.Create(AddressLine1),
            addressLine2: AddressLine.Create(AddressLine2),
            addressLine3: AddressLine.Create(AddressLine3),
            addressLine4: AddressLine.Create(AddressLine4),
            city: Domain.Common.ValueObjects.Address.City.Create(City),
            county: Domain.Common.ValueObjects.Address.County.Create(County),
            postalCode: PostalCode.Create(PostCode, parsedCountry)
        );

        return address;
    }
}