using System.ComponentModel.DataAnnotations;

namespace API.Models.Request.WarehouseMerchants;

public record CreateWarehouseMerchantRequest
{
    [Range(1, int.MaxValue)]
    public required int WarehouseId { get; init; }

    [Range(1, int.MaxValue)]
    public required int MerchantId { get; init; }

    public required int PreferenceOrder { get; init; }
    public required bool Active { get; init; }
}