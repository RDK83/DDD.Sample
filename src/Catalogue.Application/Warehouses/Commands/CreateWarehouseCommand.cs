using Catalogue.Domain.Common.Enums;
using Catalogue.Domain.Common.ValueObjects.Address;
using Catalogue.Domain.Warehouses;
using Shared.Domain.Exceptions;
using Shared.Domain.Exceptions.Helper;

namespace Catalogue.Application.Warehouses.Commands;

public record CreateWarehouseCommand(
    string WarehouseName,
    string WarehouseCode,
    string AddressLine1,
    string City,
    string PostCode,
    string CountryIsoCode,
    bool Active,
    string? AddressLine2,
    string? AddressLine3,
    string? AddressLine4,
    string? County)
{
    private Address ToAddress()
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

    public Warehouse ToEntity()
    {
        var address = ToAddress();

        var warehouse = Warehouse.Create(
            warehouseName: Domain.Warehouses.ValueObjects.WarehouseName.Create(WarehouseName),
            warehouseCode: Domain.Warehouses.ValueObjects.WarehouseCode.Create(WarehouseCode),
            address: address,
            active: Active);

        return warehouse;
    }
}