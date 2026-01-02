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

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    private ProductCode()

    {
        //EF
    }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

    public static ProductCode Create(string productCode)
    {
        DomainGuard.AgainstNullOrWhiteSpace(productCode);

        productCode = productCode.Trim().ToUpperInvariant();

        DomainGuard.AgainstStringTooLong(productCode, MaxLength);

        return new ProductCode(productCode);
    }

    public static implicit operator string(ProductCode productCode)
    {
        return productCode.Value;
    }
}