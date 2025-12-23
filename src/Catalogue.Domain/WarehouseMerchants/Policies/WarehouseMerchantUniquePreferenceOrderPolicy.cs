using Catalogue.Domain.WarehouseMerchants.ValueObjects;
using Shared.Domain.Guard;

namespace Catalogue.Domain.WarehouseMerchants.Policies;

public class WarehouseMerchantUniquePreferenceOrderPolicy
{
    private readonly IReadOnlyCollection<PreferenceOrder> _existingWarehouseMerchantPreferenceOrders;

    public WarehouseMerchantUniquePreferenceOrderPolicy(IReadOnlyCollection<PreferenceOrder> existingWarehouseMerchantPreferenceOrders)
    {
        Guard.Against.Null(existingWarehouseMerchantPreferenceOrders);

        _existingWarehouseMerchantPreferenceOrders = existingWarehouseMerchantPreferenceOrders;
    }

    public bool ValidateForUniqueness(PreferenceOrder newPreferenceOrder)
    {
        Guard.Against.Null(newPreferenceOrder);

        return !_existingWarehouseMerchantPreferenceOrders.Any(preferenceOrder => Equals(preferenceOrder, newPreferenceOrder));
    }
}