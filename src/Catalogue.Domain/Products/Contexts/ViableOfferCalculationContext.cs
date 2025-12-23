using Catalogue.Domain.Merchants.ValueObjects;
using Catalogue.Domain.WarehouseMerchants.ValueObjects;
using Catalogue.Domain.Warehouses.ValueObjects;
using Shared.Domain.Guard;

namespace Catalogue.Domain.Products.Contexts;

public record ViableOfferCalculationContext
{
    public IReadOnlyCollection<MerchantId> ActiveMerchants { get; }
    public IReadOnlyCollection<WarehouseId> AvailableWarehouses { get; }

    public IReadOnlyCollection<WarehouseMerchantStatus> EligibleWarehouseMerchants { get; }

    private ViableOfferCalculationContext(IReadOnlyCollection<MerchantId> activeMerchants,
        IReadOnlyCollection<WarehouseId> availableWarehouses,
        IReadOnlyCollection<WarehouseMerchantStatus> eligibleWarehouseMerchants)
    {
        ActiveMerchants = activeMerchants;
        AvailableWarehouses = availableWarehouses;
        EligibleWarehouseMerchants = eligibleWarehouseMerchants;
    }

    public static ViableOfferCalculationContext Create(
        IReadOnlyCollection<MerchantId> eligibleMerchants,
        IReadOnlyCollection<WarehouseId> eligibleWarehouses,
        IReadOnlyCollection<WarehouseMerchantStatus> eligibleWarehouseMerchants)
    {
        var eligibleMerchantsAsReadOnly = Guard.Against.Null(eligibleMerchants);
        var eligibleWarehousesAsReadOnly = Guard.Against.Null(eligibleWarehouses);
        var eligibleWarehouseMerchantsAsReadOnly = Guard.Against.Null(eligibleWarehouseMerchants);

        return new ViableOfferCalculationContext(
            eligibleMerchantsAsReadOnly,
            eligibleWarehousesAsReadOnly,
            eligibleWarehouseMerchantsAsReadOnly
        );
    }
}