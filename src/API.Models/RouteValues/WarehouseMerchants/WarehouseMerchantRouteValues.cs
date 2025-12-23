using System.ComponentModel.DataAnnotations;

namespace API.Models.RouteValues.WarehouseMerchants;

public record WarehouseMerchantRouteValues : IRouteValues
{
    [Range(1, int.MaxValue)]
    public required int WarehouseMerchantId { get; init; }
}