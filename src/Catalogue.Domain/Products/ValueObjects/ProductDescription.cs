using Shared.Domain.Aggregates.ValueObjects;
using Shared.Domain.Guard;
using ValidationRules.Products;

namespace Catalogue.Domain.Products.ValueObjects;

public record ProductDescription : IStringValueObject<ProductDescription>
{
    public string Value { get; }
    public static int MaxLength => ProductValidationRules.DescriptionMaxLength;

    protected ProductDescription(string value)
    {
        Value = value;
    }

    public static ProductDescription Create(string productDescription)
    {
        DomainGuard.AgainstNullOrWhiteSpace(productDescription);

        productDescription = productDescription.Trim();

        DomainGuard.AgainstStringTooLong(productDescription, MaxLength);

        return new ProductDescription(productDescription);
    }

    public static implicit operator string(ProductDescription productDescription)
    {
        return productDescription.Value;
    }
}