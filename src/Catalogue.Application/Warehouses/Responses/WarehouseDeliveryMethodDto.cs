using Catalogue.Domain.Warehouses.Entities;

namespace Catalogue.Application.Warehouses.Responses;

public record WarehouseDeliveryMethodDto(
    int WarehouseId,
    string DeliveryMethodId,
    string DeliveryMethodName,
    string DeliveryCountryIsoCode,
    decimal GrossCost,
    string CurrencyIsoCode,
    byte MinimumLeadTime,
    byte MaximumLeadTime,
    bool Active
)
{
    internal static WarehouseDeliveryMethodDto FromEntity(WarehouseDeliveryMethod entity)
    {
        var dto = new WarehouseDeliveryMethodDto(
            WarehouseId: entity.WarehouseId,
            DeliveryMethodId: entity.DeliveryMethodId,
            DeliveryMethodName: entity.DeliveryMethod.Name,
            DeliveryCountryIsoCode: entity.CountryIsoCode.ToString(),
            CurrencyIsoCode: entity.GrossCost.Currency.ToString(),
            GrossCost: entity.GrossCost.Value,
            MinimumLeadTime: entity.LeadTimes.MinimumLeadTime,
            MaximumLeadTime: entity.LeadTimes.MaximumLeadTime,
            Active: entity.Active
        );

        return dto;
    }
}