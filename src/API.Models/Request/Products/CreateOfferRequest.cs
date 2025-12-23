using System.ComponentModel.DataAnnotations;
using ValidationRules.Products;

namespace API.Models.Request.Products;

public record CreateOfferRequest
{
    [MinLength(OfferValidationRules.CurrencyIsoCodeLength)]
    [MaxLength(OfferValidationRules.CurrencyIsoCodeLength)]
    public required string CurrencyIsoCode { get; init; }

    public required bool Active { get; init; }


    public required int MerchantId { get; init; }
    public required int WarehouseId { get; init; }
    public required int StockLevel { get; init; }

    public required decimal GrossPrice { get; init; }
}