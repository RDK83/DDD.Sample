using System.ComponentModel.DataAnnotations;
using ValidationRules.Products;

namespace API.Models.RouteValues.Products;

public record ProductRouteValues : IRouteValues
{
    [MaxLength(ProductValidationRules.ProductCodeMaxLength)]
    public required string ProductCode { get; init; }
}