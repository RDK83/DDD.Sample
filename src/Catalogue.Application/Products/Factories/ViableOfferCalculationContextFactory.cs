using Catalogue.Application.Products.Contexts;
using Catalogue.Domain.Products;
using Catalogue.Domain.Products.Contexts;
using Catalogue.Domain.Products.Policies;

namespace Catalogue.Application.Products.Factories;

public class ViableOfferCalculationContextFactory
{
    public static ViableOfferCalculationContext Build(Product product, OfferDependencies dependencies)
    {
        var warehouseMerchantStatuses =
            dependencies.ActiveWarehouseMerchants.Select(x => x.ToWarehouseMerchantStatus()).ToArray();

        var policy = new ProductToWarehouseMerchantValidityPolicy();
        var validWarehouseMerchants = policy.DetermineValidForProduct(warehouseMerchantStatuses, product);

        var activeMerchantIds = dependencies.ActiveMerchants.Select(x => x.Id).ToArray();
        var availableWarehouseIds = dependencies.ActiveWarehouses.Where(w => w.IsAvailable())
            .Select(w => w.Id).ToArray();

        return ViableOfferCalculationContext.Create(
            activeMerchantIds,
            availableWarehouseIds,
            validWarehouseMerchants
        );
    }
}