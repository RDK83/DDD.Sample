using Shared.Domain.Aggregates.ValueObjects;
using Shared.Domain.Guard;
using ValidationRules.Products;

namespace Catalogue.Domain.Products.ValueObjects;

public record ProductTitle : IStringValueObject<ProductTitle>
{
    public string Value { get; }
    public static int MaxLength => ProductValidationRules.TitleMaxLength;

    protected ProductTitle(string value)
    {
        Value = value;
    }

    public static ProductTitle Create(string productTitle)
    {
        Guard.Against.NullOrWhiteSpace(productTitle);

        productTitle = productTitle.Trim();

        Guard.Against.StringTooLong(productTitle, MaxLength);

        return new ProductTitle(productTitle);
    }


    public static implicit operator string(ProductTitle productTitle)
    {
        return productTitle.Value;
    }
}