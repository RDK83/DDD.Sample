using Shared.Domain.Aggregates.ValueObjects;
using Shared.Domain.Exceptions;
using Shared.Domain.Guard;

namespace Catalogue.Domain.Warehouses.ValueObjects;

public record WarehouseDeliveryLeadTime : IValueObject<WarehouseDeliveryLeadTime, byte, byte>
{
    private WarehouseDeliveryLeadTime()
    {
        //EF OwnedType
    }

    public byte MinimumLeadTime { get; }

    public byte MaximumLeadTime { get; }

    protected WarehouseDeliveryLeadTime(byte minLeadTime, byte maxLeadTime)
    {
        MinimumLeadTime = minLeadTime;
        MaximumLeadTime = maxLeadTime;
    }

    public static WarehouseDeliveryLeadTime Create(byte minLeadTime, byte maxLeadTime)
    {
        Guard.Against.Zero(minLeadTime);

        if (minLeadTime > maxLeadTime)
            throw new DomainValidationException("Min Lead Time cannot be greater than Max Lead Time", nameof(minLeadTime));

        return new WarehouseDeliveryLeadTime(minLeadTime, maxLeadTime);
    }
}