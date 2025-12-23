namespace API.Models.Request.Products;

public record UpdateOfferRequest
{
    public required bool Active { get; init; }

    public required decimal GrossPrice { get; init; }

    public required int StockLevel { get; init; }
}