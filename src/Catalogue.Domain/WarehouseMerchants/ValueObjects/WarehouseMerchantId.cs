using Shared.Domain.Aggregates.ValueObjects;
using Shared.Domain.Guard;

namespace Catalogue.Domain.WarehouseMerchants.ValueObjects;

public record WarehouseMerchantId : IIntegerValueObject<WarehouseMerchantId>
{
    public int Value { get; }

    protected WarehouseMerchantId(int value)
    {
        Value = value;
    }

    public static WarehouseMerchantId Create(int warehouseMerchantId)
    {
        DomainGuard.AgainstZero(warehouseMerchantId);

        return new WarehouseMerchantId(warehouseMerchantId);
    }


    public static implicit operator int(WarehouseMerchantId warehouseMerchantId)
    {
        return warehouseMerchantId.Value;
    }
}