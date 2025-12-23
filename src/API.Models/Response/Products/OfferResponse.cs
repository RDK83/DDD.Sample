namespace API.Models.Response.Products;

public record OfferResponse(
    int Id,
    int WarehouseId,
    int MerchantId,
    string ProductCode,
    decimal GrossPrice,
    string Currency,
    int StockLevel
);