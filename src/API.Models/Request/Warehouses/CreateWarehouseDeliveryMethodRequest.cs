using System.ComponentModel.DataAnnotations;
using ValidationRules.Warehouses;

namespace API.Models.Request.Warehouses;

public record CreateWarehouseDeliveryMethodRequest
{
    [MaxLength(WarehouseDeliveryMethodValidationRules.DeliveryMethodIdLength)]
    [MinLength(WarehouseDeliveryMethodValidationRules.DeliveryMethodIdLength)]
    public required string DeliveryMethodId { get; init; }
    [MaxLength(WarehouseDeliveryMethodValidationRules.CurrencyIsoCodeLength)]
    [MinLength(WarehouseDeliveryMethodValidationRules.CurrencyIsoCodeLength)]
    public required string DeliveryCountryIsoCode { get; init; }
}