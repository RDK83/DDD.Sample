using Catalogue.Domain.WarehouseMerchants.Mutations;

namespace Catalogue.Application.WarehouseMerchants.Commands;

public record UpdateWarehouseMerchantCommand(int Id, int PreferenceOrder, bool Active)
{
    public EditWarehouseMerchantMutation ToEditValues()
    {
        return new EditWarehouseMerchantMutation(
            PreferenceOrder: Domain.WarehouseMerchants.ValueObjects.PreferenceOrder.Create(PreferenceOrder),
            Active: Active);
    }
}