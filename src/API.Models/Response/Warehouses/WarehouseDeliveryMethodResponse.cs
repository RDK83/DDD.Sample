namespace API.Models.Response.Warehouses;

public sealed record WarehouseDeliveryMethodResponse(
    int WarehouseId,
    string DeliveryMethodId,
    string DeliverMethodName,
    string DeliveryCountryIsoCode,
    decimal GrossCost,
    string CurrencyIsoCode,
    byte MinimumLeadTime,
    byte MaximumLeadTime,
    bool Active
);