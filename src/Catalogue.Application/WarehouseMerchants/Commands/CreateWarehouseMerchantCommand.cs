using Catalogue.Domain.WarehouseMerchants;
using Catalogue.Domain.WarehouseMerchants.Policies;
using Catalogue.Domain.WarehouseMerchants.ValueObjects;

namespace Catalogue.Application.WarehouseMerchants.Commands;

public record CreateWarehouseMerchantCommand(
    int WarehouseId,
    int MerchantId,
    bool Active,
    int PreferenceOrder,
    IEnumerable<PreferenceOrder> ExistingPreferenceOrders)
{
    public WarehouseMerchant ToEntity(
        WarehouseMerchantUniquePreferenceOrderPolicy warehouseMerchantUniquePreferenceOrderPolicy)
    {
        return WarehouseMerchant.Create(
            warehouseId: Domain.Warehouses.ValueObjects.WarehouseId.Create(WarehouseId),
            merchantId: Domain.Merchants.ValueObjects.MerchantId.Create(MerchantId),
            preferenceOrder: Domain.WarehouseMerchants.ValueObjects.PreferenceOrder.Create(PreferenceOrder),
            active: Active,
            uniquePreferenceOrderPolicy: warehouseMerchantUniquePreferenceOrderPolicy);
    }
}