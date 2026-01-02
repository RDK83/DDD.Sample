using Shared.Domain.Aggregates.ValueObjects;
using Shared.Domain.Guard;

namespace Catalogue.Domain.Products.ValueObjects;

public readonly record struct StockLevel : IIntegerValueObject<StockLevel>
{
    public int Value { get; }

    private StockLevel(int value)
    {
        Value = value;
    }

    public static StockLevel Create(int input)
    {
        DomainGuard.AgainstNegative(input);

        DomainGuard.AgainstOutOfRange(input, 0, 3000000);

        return new StockLevel(input);
    }

    public static implicit operator int(StockLevel stockLevel)
    {
        return stockLevel.Value;
    }
}