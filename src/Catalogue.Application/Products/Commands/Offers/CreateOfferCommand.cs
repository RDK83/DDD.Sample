using Catalogue.Domain.Common.ValueObjects;
using Catalogue.Domain.Products.Mutations;

namespace Catalogue.Application.Products.Commands.Offers;

public record CreateOfferCommand(
    string ProductCode,
    string PublishedVersion,
    int MerchantId,
    int StockLevel,
    int WarehouseId,
    decimal GrossPrice
)
{
    public NewOfferMutation ToMutation()
    {
        var newOfferValues = new NewOfferMutation(
            ProductCode: Domain.Products.ValueObjects.ProductCode.Create(ProductCode),
            WarehouseId: Domain.Warehouses.ValueObjects.WarehouseId.Create(WarehouseId),
            MerchantId: Domain.Merchants.ValueObjects.MerchantId.Create(MerchantId),
            GrossPrice: Money.Create(GrossPrice),
            StockLevel: Domain.Products.ValueObjects.StockLevel.Create(StockLevel)
        );

        return newOfferValues;
    }
}