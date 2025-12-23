using Shared.Domain.Aggregates.ValueObjects;
using Shared.Domain.Guard;

namespace Catalogue.Domain.Warehouses.ValueObjects;

public readonly record struct WarehouseId : IIntegerValueObject<WarehouseId>
{
    public int Value { get; }

    private WarehouseId(int value)
    {
        Value = value;
    }

    public static WarehouseId Create(int warehouseId)
    {
        Guard.Against.Zero(warehouseId);

        return new WarehouseId(warehouseId);
    }

    public static implicit operator int(WarehouseId warehouseId)
    {
        return warehouseId.Value;
    }
}