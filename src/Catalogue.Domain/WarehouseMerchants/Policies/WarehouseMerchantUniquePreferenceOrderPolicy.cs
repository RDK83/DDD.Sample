using Catalogue.Domain.WarehouseMerchants.ValueObjects;
using Shared.Domain.Guard;

namespace Catalogue.Domain.WarehouseMerchants.Policies;

public class WarehouseMerchantUniquePreferenceOrderPolicy
{
    private readonly IReadOnlyCollection<PreferenceOrder> _existingWarehouseMerchantPreferenceOrders;

    public WarehouseMerchantUniquePreferenceOrderPolicy(
        IReadOnlyCollection<PreferenceOrder> existingWarehouseMerchantPreferenceOrders)
    {
        DomainGuard.AgainstNull(existingWarehouseMerchantPreferenceOrders);

        _existingWarehouseMerchantPreferenceOrders = existingWarehouseMerchantPreferenceOrders;
    }

    public bool ValidateForUniqueness(PreferenceOrder newPreferenceOrder)
    {
        DomainGuard.AgainstNull(newPreferenceOrder);

        return !_existingWarehouseMerchantPreferenceOrders.Any(preferenceOrder =>
            Equals(preferenceOrder, newPreferenceOrder));
    }
}