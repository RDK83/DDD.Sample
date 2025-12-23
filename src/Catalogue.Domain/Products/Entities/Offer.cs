using Catalogue.Domain.Common.ValueObjects;
using Catalogue.Domain.Merchants.ValueObjects;
using Catalogue.Domain.Products.Contexts;
using Catalogue.Domain.Products.Mutations;
using Catalogue.Domain.Products.ValueObjects;
using Catalogue.Domain.Warehouses.ValueObjects;
using Shared.Domain.Aggregates.Entities;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

namespace Catalogue.Domain.Products.Entities;

public class Offer : BaseEntity<OfferId>
{
    private Offer()
    {
        //required by EF as this entity has Owned types in config
    }

    public ProductCode ProductCode { get; }

    public WarehouseId WarehouseId { get; }

    public MerchantId MerchantId { get; }

    public Money GrossPrice { get; private set; }

    public StockLevel StockLevel { get; private set; }


    protected Offer(ProductCode productCode, WarehouseId warehouseId, MerchantId merchantId, Money grossPrice,
        StockLevel stockLevel)
    {
        WarehouseId = warehouseId;
        MerchantId = merchantId;
        ProductCode = productCode;
        GrossPrice = grossPrice;
        StockLevel = stockLevel;
    }

    internal static Offer Create(ProductCode productCode, WarehouseId warehouseId, MerchantId merchantId,
        Money grossPrice, StockLevel stockLevel)
    {
        return new Offer(
            productCode,
            warehouseId,
            merchantId,
            grossPrice,
            stockLevel
        );
    }

    internal bool IsViable(ViableOfferCalculationContext context)
    {
        if (!context.AvailableWarehouses.Any(x => x.Equals(WarehouseId)))
            return false;

        var viable = context.ActiveMerchants.Contains(MerchantId)
                     && context.AvailableWarehouses.Contains(WarehouseId)
                     && context.EligibleWarehouseMerchants.Any(wm =>
                         wm.MerchantId.Equals(MerchantId) && wm.WarehouseId.Equals(WarehouseId))
                     && StockLevel > 0;

        return viable;
    }

    internal void Update(EditOfferMutation values)
    {
        GrossPrice = values.GrossPrice;
        StockLevel = values.StockLevel;
    }
}