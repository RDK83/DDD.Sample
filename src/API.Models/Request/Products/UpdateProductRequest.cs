using System.ComponentModel.DataAnnotations;
using ValidationRules.Common;
using ValidationRules.Products;

namespace API.Models.Request.Products;

public record UpdateProductRequest
{
    [MaxLength(ProductValidationRules.DescriptionMaxLength)]
    public required string ShortDescription { get; init; }


    [MaxLength(ProductValidationRules.TitleMaxLength)]
    public required string ProductTitle { get; init; }


    [EnumDataType(typeof(TaxClassValidationRules.TaxClass))]
    public required string TaxClass { get; init; }


    public int ProductTypeId { get; init; }
    public int ProductSubTypeId { get; init; }
    public int ManufacturerClassificationId { get; init; }
}