using Shared.Domain.Aggregates.ValueObjects;
using Shared.Domain.Guard;

namespace Catalogue.Domain.ProductTypes.ValueObjects;

public readonly record struct ProductTypeId : IIntegerValueObject<ProductTypeId>
{
    public int Value { get; }

    private ProductTypeId(int value)
    {
        Value = value;
    }

    public static ProductTypeId Create(int productTypeId)
    {
        DomainGuard.AgainstZero(productTypeId);

        return new ProductTypeId(productTypeId);
    }

    public static implicit operator int(ProductTypeId typeId)
    {
        return typeId.Value;
    }
}