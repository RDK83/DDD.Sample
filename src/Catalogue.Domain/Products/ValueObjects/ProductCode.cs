using Shared.Domain.Aggregates.ValueObjects;
using Shared.Domain.Guard;
using ValidationRules.Products;

namespace Catalogue.Domain.Products.ValueObjects;

public record ProductCode : IStringValueObject<ProductCode>
{
    public string Value { get; }

    public static int MaxLength => ProductValidationRules.ProductCodeMaxLength;

    protected ProductCode(string value)
    {
        Value = value;
    }

    private ProductCode()
    {
        //EF
    }

    public static ProductCode Create(string productCode)
    {
        Guard.Against.NullOrWhiteSpace(productCode);

        productCode = productCode.Trim().ToUpperInvariant();

        Guard.Against.StringTooLong(productCode, MaxLength);

        return new ProductCode(productCode);
    }

    public static implicit operator string(ProductCode productCode)
    {
        return productCode.Value;
    }
}