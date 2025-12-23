using System.ComponentModel.DataAnnotations;
using ValidationRules.Common;
using ValidationRules.Products;

namespace API.Models.Request.Products;

public record CreateProductRequest
{
    //ids
    [MaxLength(ProductValidationRules.ProductCodeMaxLength)]
    public required string ProductCode { get; init; }

    [MaxLength(ProductValidationRules.TitleMaxLength)]
    public required string ProductTitle { get; init; }

    [MaxLength(ProductValidationRules.DescriptionMaxLength)]
    public required string ShortDescription { get; init; }


    // integers
    public required int ManufacturerClassificationId { get; init; }
    public required int ProductTypeId { get; init; }
    public required int ProductSubTypeId { get; init; }


    // enums
    [EnumDataType(typeof(TaxClassValidationRules.TaxClass))]
    public required string TaxClass { get; init; }
}