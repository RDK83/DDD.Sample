namespace API.Models.Request.WarehouseMerchants;

public record UpdateWarehouseMerchantRequest
{
    public required int PreferenceOrder { get; init; }
    public required bool Active { get; init; }
}