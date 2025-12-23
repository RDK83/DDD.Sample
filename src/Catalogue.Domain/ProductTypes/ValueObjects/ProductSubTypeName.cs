using Shared.Domain.Aggregates.ValueObjects;
using Shared.Domain.Guard;
using ValidationRules.ProductTypes;

namespace Catalogue.Domain.ProductTypes.ValueObjects;

public record ProductSubTypeName : IStringValueObject<ProductSubTypeName>
{
    public string Value { get; }
    public static int MaxLength => ProductSubTypeValidationRules.ProductSubTypeNameMaxLength;

    protected ProductSubTypeName(string value)
    {
        Value = value;
    }

    public static ProductSubTypeName Create(string productTypeName)
    {
        Guard.Against.NullOrWhiteSpace(productTypeName);

        productTypeName = productTypeName.Trim();

        Guard.Against.StringTooLong(productTypeName, MaxLength);

        return new ProductSubTypeName(productTypeName);
    }

    public static implicit operator string(ProductSubTypeName productSubTypeName)
    {
        return productSubTypeName.Value;
    }
}