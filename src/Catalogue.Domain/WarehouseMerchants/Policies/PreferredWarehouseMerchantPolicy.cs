using Catalogue.Domain.Merchants.ValueObjects;
using Catalogue.Domain.WarehouseMerchants.ValueObjects;
using Shared.Domain.Exceptions;
using Shared.Domain.Guard;

namespace Catalogue.Domain.WarehouseMerchants.Policies;

public class PreferredWarehouseMerchantPolicy
{
    public IReadOnlyCollection<WarehouseMerchantStatus> DeterminePreferredWarehouseMerchants(
        IReadOnlyCollection<WarehouseMerchantStatus> warehouseMerchants)
    {
        Guard.Against.Null(warehouseMerchants);

        var merchantPreferences = new Dictionary<MerchantId, WarehouseMerchantStatus>();

        foreach (var eligibleWarehouseMerchant in warehouseMerchants)
        {
            if (merchantPreferences.TryGetValue(eligibleWarehouseMerchant.MerchantId, out var warehouseMerchantStatus))
            {
                if (eligibleWarehouseMerchant.PreferenceOrder < warehouseMerchantStatus.PreferenceOrder)
                    merchantPreferences[eligibleWarehouseMerchant.MerchantId] = eligibleWarehouseMerchant;

                continue;
            }

            merchantPreferences[eligibleWarehouseMerchant.MerchantId] = eligibleWarehouseMerchant;
        }

        return merchantPreferences.Values.ToList();
    }

    public WarehouseMerchantStatus DetermineMostPreferredWarehouseMerchant(
        IReadOnlyCollection<WarehouseMerchantStatus> warehouseMerchants)
    {
        Guard.Against.Null(warehouseMerchants);

        var preferredWarehouseMerchants =
            DeterminePreferredWarehouseMerchants(warehouseMerchants).OrderBy(wm => wm.PreferenceOrder);

        if (!preferredWarehouseMerchants.Any())
            throw new PolicyException(nameof(PreferredWarehouseMerchantPolicy),
                "Cannot determine Most Preferred WarehouseMerchant as there were no Preferred WarehouseMerchants.");

        var promotedWarehouseMerchant = DeterminePreferredWarehouseMerchants(warehouseMerchants)
            .OrderBy(wm => wm.PreferenceOrder).First();

        return promotedWarehouseMerchant;
    }
}