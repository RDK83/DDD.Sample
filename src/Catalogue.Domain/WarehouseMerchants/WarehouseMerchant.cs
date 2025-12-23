using Catalogue.Domain.Merchants.ValueObjects;
using Catalogue.Domain.WarehouseMerchants.Mutations;
using Catalogue.Domain.WarehouseMerchants.Policies;
using Catalogue.Domain.WarehouseMerchants.ValueObjects;
using Catalogue.Domain.Warehouses.ValueObjects;
using Shared.Domain.Aggregates;
using Shared.Domain.Exceptions;

namespace Catalogue.Domain.WarehouseMerchants;

public class WarehouseMerchant : BaseAggregate<WarehouseMerchantId>
{
    public WarehouseId WarehouseId { get; }

    public MerchantId MerchantId { get; }

    public PreferenceOrder PreferenceOrder { get; private set; }
    public bool Active { get; private set; }


    private protected WarehouseMerchant(WarehouseId warehouseId, MerchantId merchantId, PreferenceOrder preferenceOrder,
        bool active)
    {
        WarehouseId = warehouseId;
        MerchantId = merchantId;
        PreferenceOrder = preferenceOrder;
        Active = active;
    }

    public void Update(EditWarehouseMerchantMutation values,
        WarehouseMerchantUniquePreferenceOrderPolicy uniquePreferenceOrderPolicy)
    {
        Active = values.Active;

        if (PreferenceOrder.Equals(values.PreferenceOrder))
            return;

        var isValid = uniquePreferenceOrderPolicy.ValidateForUniqueness(values.PreferenceOrder);

        if (!isValid)
            throw new DomainValidationException(
                "The preference order must be unique among existing warehouse merchants when Updating.");

        PreferenceOrder = values.PreferenceOrder;
    }


    public static WarehouseMerchant Create(WarehouseId warehouseId, MerchantId merchantId,
        PreferenceOrder preferenceOrder, bool active,
        WarehouseMerchantUniquePreferenceOrderPolicy uniquePreferenceOrderPolicy)
    {
        var isValid = uniquePreferenceOrderPolicy.ValidateForUniqueness(preferenceOrder);

        return isValid
            ? new WarehouseMerchant(warehouseId, merchantId, preferenceOrder, active)
            : throw new DomainValidationException(
                "The preference order must be unique among existing warehouse merchants when Creating.");
    }

    public WarehouseMerchantStatus ToWarehouseMerchantStatus()
    {
        return WarehouseMerchantStatus.Create(WarehouseId, MerchantId, PreferenceOrder, Active);
    }
}