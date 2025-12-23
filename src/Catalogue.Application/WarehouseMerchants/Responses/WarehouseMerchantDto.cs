using Catalogue.Domain.WarehouseMerchants;

namespace Catalogue.Application.WarehouseMerchants.Responses;

public record WarehouseMerchantDto(int WarehouseId, int MerchantId, bool Active, int PreferenceOrder, int Id)
{
    public static WarehouseMerchantDto FromEntity(WarehouseMerchant entity)
    {
        return new WarehouseMerchantDto
        (
            WarehouseId: entity.WarehouseId,
            MerchantId: entity.MerchantId,
            Active: entity.Active,
            PreferenceOrder: entity.PreferenceOrder.Value,
            Id: entity.Id
        );
    }
}