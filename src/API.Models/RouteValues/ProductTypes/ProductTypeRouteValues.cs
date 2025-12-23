using System.ComponentModel.DataAnnotations;

namespace API.Models.RouteValues.ProductTypes;

public class ProductTypeRouteValues : IRouteValues
{
    [Range(1, int.MaxValue)]
    public required int ProductTypeId { get; init; }
}