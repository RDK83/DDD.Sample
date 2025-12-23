using System.ComponentModel.DataAnnotations;
using API.Models.Pagination.Abstract;
using ValidationRules.Products;

namespace API.Models.Request.Products;

public record ProductKeysetPagingRequest : BaseKeysetPagingRequest, IHasKeysetCursor<string>
{
    [MinLength(0)]
    [MaxLength(ProductValidationRules.ProductCodeMaxLength)]
    public string? Cursor { get; init; } = null;
}