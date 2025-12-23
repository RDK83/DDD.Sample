using System.ComponentModel.DataAnnotations;
using ValidationRules.Products;

namespace API.Models.Request.Products;

public record DeleteProductRequest
{
    [MaxLength(ProductValidationRules.ProductCodeMaxLength)]
    public required string ProductCode { get; init; }
}