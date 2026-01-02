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
        DomainGuard.AgainstNullOrWhiteSpace(productTypeName);

        productTypeName = productTypeName.Trim();

        DomainGuard.AgainstStringTooLong(productTypeName, MaxLength);

        return new ProductTypeName(productTypeName);
    }

    public static implicit operator string(ProductTypeName productTypeName)
    {
        return productTypeName.Value;
    }
}