namespace API.Models.Response.WarehouseMerchants;

public record WarehouseMerchantResponse(int WarehouseMerchantId, int WarehouseId, int MerchantId, bool Active, int PreferenceOrder);