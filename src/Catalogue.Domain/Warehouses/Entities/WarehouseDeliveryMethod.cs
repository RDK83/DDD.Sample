using Catalogue.Domain.Common.Enums;
using Catalogue.Domain.Common.ValueObjects;
using Catalogue.Domain.Lookups.DeliveryMethods;
using Catalogue.Domain.Lookups.DeliveryMethods.ValueObjects;
using Catalogue.Domain.Warehouses.ValueObjects;
using Shared.Domain.Aggregates.Entities;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

namespace Catalogue.Domain.Warehouses.Entities;

public class WarehouseDeliveryMethod : BaseEntity<WarehouseDeliveryMethodId>
{
    private WarehouseDeliveryMethod()
    {
        //EF
    }

    public WarehouseId WarehouseId { get; private set; }

    public DeliveryMethodId DeliveryMethodId { get; private set; }

    public Country CountryIsoCode { get; private set; }

    public Money GrossCost { get; private set; }

    public WarehouseDeliveryLeadTime LeadTimes { get; private set; }

    public bool Active { get; private set; }

    public DeliveryMethod DeliveryMethod { get; private set; }


    protected WarehouseDeliveryMethod(
        WarehouseId warehouseId,
        DeliveryMethodId deliveryMethodId,
        Country countryIsoCode,
        Money grossCost,
        WarehouseDeliveryLeadTime leadTimes,
        bool active)
    {
        WarehouseId = warehouseId;
        DeliveryMethodId = deliveryMethodId;
        CountryIsoCode = countryIsoCode;
        GrossCost = grossCost;
        LeadTimes = leadTimes;
        Active = active;
    }


    internal static WarehouseDeliveryMethod Create(
        WarehouseId warehouseId,
        DeliveryMethodId deliveryMethodId,
        Country countryIsoCode,
        Money grossCost,
        WarehouseDeliveryLeadTime leadTimes,
        bool active)
    {
        return new WarehouseDeliveryMethod(
            warehouseId,
            deliveryMethodId,
            countryIsoCode,
            grossCost,
            leadTimes,
            active
        );
    }
}