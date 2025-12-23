namespace API.Models.Response.Warehouses;

public sealed record WarehouseResponse(
    int Id,
    string WarehouseName,
    string WarehouseCode,
    bool Active,
    string AddressLine1,
    string City,
    string CountryIsoCode,
    string Postcode,
    string? AddressLine2,
    string? AddressLine3,
    string? AddressLine4,
    string? County,
    IReadOnlyCollection<WarehouseDeliveryMethodResponse>? WarehouseDeliveryMethods
);