using Shared.Domain.Aggregates.ValueObjects;
using Shared.Domain.Guard;

namespace Catalogue.Domain.Warehouses.ValueObjects;

public readonly record struct WarehouseDeliveryMethodId : IIntegerValueObject<WarehouseDeliveryMethodId>
{
    public int Value { get; }

    private WarehouseDeliveryMethodId(int value)
    {
        Value = value;
    }

    public static WarehouseDeliveryMethodId Create(int warehouseDeliveryMethodId)
    {
        DomainGuard.AgainstZero(warehouseDeliveryMethodId);

        return new WarehouseDeliveryMethodId(warehouseDeliveryMethodId);
    }

    public static implicit operator int(WarehouseDeliveryMethodId warehouseDeliveryMethodId)
    {
        return warehouseDeliveryMethodId.Value;
    }
}