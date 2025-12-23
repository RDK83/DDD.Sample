using System.ComponentModel.DataAnnotations;

namespace API.Models.RouteValues.Warehouses;

public record WarehouseRouteValues : IRouteValues
{
    [Range(1, int.MaxValue)]
    public required int WarehouseId { get; init; }
}