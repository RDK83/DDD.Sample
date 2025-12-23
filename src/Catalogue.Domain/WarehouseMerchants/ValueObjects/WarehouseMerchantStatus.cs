using Catalogue.Domain.Merchants.ValueObjects;
using Catalogue.Domain.Warehouses.ValueObjects;
using Shared.Domain.Aggregates.ValueObjects;

namespace Catalogue.Domain.WarehouseMerchants.ValueObjects;

public record WarehouseMerchantStatus : IValueObject
{
    public WarehouseId WarehouseId { get; }
    public MerchantId MerchantId { get; }
    public PreferenceOrder PreferenceOrder { get; }
    public bool Active { get; }


    protected WarehouseMerchantStatus(WarehouseId warehouseId, MerchantId merchantId, PreferenceOrder preferenceOrder,
        bool active)
    {
        WarehouseId = warehouseId;
        MerchantId = merchantId;
        PreferenceOrder = preferenceOrder;
        Active = active;
    }

    public static WarehouseMerchantStatus Create(WarehouseId warehouseId, MerchantId merchantId,
        PreferenceOrder preferenceOrder, bool active)
    {
        return new WarehouseMerchantStatus(warehouseId, merchantId, preferenceOrder, active);
    }
}