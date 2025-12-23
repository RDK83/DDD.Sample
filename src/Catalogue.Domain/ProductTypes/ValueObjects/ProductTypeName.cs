using Shared.Domain.Aggregates.ValueObjects;
using Shared.Domain.Guard;
using ValidationRules.ProductTypes;

namespace Catalogue.Domain.ProductTypes.ValueObjects;

public record ProductTypeName : IStringValueObject<ProductTypeName>
{
    public string Value { get; }
    public static int MaxLength => ProductTypeValidationRules.ProductTypeNameMaxLength;

    protected ProductTypeName(string value)
    {
        Value = value;
    }

    public static ProductTypeName Create(string productTypeName)
    {
        Guard.Against.NullOrWhiteSpace(productTypeName);

        productTypeName = productTypeName.Trim();

        Guard.Against.StringTooLong(productTypeName, MaxLength);

        return new ProductTypeName(productTypeName);
    }

    public static implicit operator string(ProductTypeName productTypeName)
    {
        return productTypeName.Value;
    }
}