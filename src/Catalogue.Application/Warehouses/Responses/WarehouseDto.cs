using Catalogue.Domain.Warehouses;

namespace Catalogue.Application.Warehouses.Responses;

public record WarehouseDto(
    int Id,
    string WarehouseName,
    string WarehouseCode,
    bool Active,
    string AddressLine1,
    string? AddressLine2,
    string? AddressLine3,
    string? AddressLine4,
    string? County,
    string City,
    string PostCode,
    string CountryIsoCode,
    IReadOnlyCollection<WarehouseDeliveryMethodDto>? WarehouseDeliveryMethods
)
{
    internal static WarehouseDto FromEntity(Warehouse warehouse)
    {
        var dto = new WarehouseDto(
            Id: warehouse.Id,
            WarehouseCode: warehouse.WarehouseCode,
            WarehouseName: warehouse.WarehouseName,
            Active: warehouse.Active,
            AddressLine1: warehouse.Address.AddressLine1,
            AddressLine2: warehouse.Address.AddressLine2,
            AddressLine3: warehouse.Address.AddressLine3,
            AddressLine4: warehouse.Address.AddressLine4,
            County: warehouse.Address.County,
            City: warehouse.Address.City,
            PostCode: warehouse.Address.PostalCode.Postcode,
            CountryIsoCode: warehouse.Address.PostalCode.CountryId.ToString(),
            WarehouseDeliveryMethods: warehouse.WarehouseDeliveryMethods.Select(WarehouseDeliveryMethodDto.FromEntity)
                .ToList()
        );

        return dto;
    }
}