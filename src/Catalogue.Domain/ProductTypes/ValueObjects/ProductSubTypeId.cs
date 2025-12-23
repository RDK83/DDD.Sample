using Shared.Domain.Aggregates.ValueObjects;
using Shared.Domain.Guard;

namespace Catalogue.Domain.ProductTypes.ValueObjects;

public readonly record struct ProductSubTypeId : IIntegerValueObject<ProductSubTypeId>
{
    public int Value { get; }

    private ProductSubTypeId(int value)
    {
        Value = value;
    }

    public static ProductSubTypeId Create(int productTypeId)
    {
        Guard.Against.Zero(productTypeId);

        return new ProductSubTypeId(productTypeId);
    }

    public static implicit operator int(ProductSubTypeId subTypeId)
    {
        return subTypeId.Value;
    }
}