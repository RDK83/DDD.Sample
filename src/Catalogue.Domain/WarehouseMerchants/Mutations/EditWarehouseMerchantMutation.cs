using Catalogue.Domain.WarehouseMerchants.ValueObjects;

namespace Catalogue.Domain.WarehouseMerchants.Mutations;

public record EditWarehouseMerchantMutation(PreferenceOrder PreferenceOrder, bool Active);