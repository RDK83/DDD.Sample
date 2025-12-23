using Catalogue.Domain.Products.Entities;

namespace Catalogue.Application.Products.Responses;

public record OfferDto(
    int Id,
    string Currency,
    string ProductCode,
    int MerchantId,
    int StockLevel,
    int WarehouseId,
    decimal GrossPrice
)
{
    public static OfferDto FromEntity(Offer offer)
    {
        return new OfferDto(
            Id: offer.Id,
            ProductCode: offer.ProductCode,
            WarehouseId: offer.WarehouseId,
            MerchantId: offer.MerchantId,
            Currency: offer.GrossPrice.Currency.ToString(),
            GrossPrice: offer.GrossPrice.Value,
            StockLevel: offer.StockLevel
        );
    }
}