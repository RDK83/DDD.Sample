using Catalogue.Domain.WarehouseMerchants.ValueObjects;
using Shared.Domain.Guard;

namespace Catalogue.Domain.Products.Policies;

public class ProductToWarehouseMerchantValidityPolicy
{
    public IReadOnlyCollection<WarehouseMerchantStatus> DetermineValidForProduct(
        IReadOnlyCollection<WarehouseMerchantStatus> warehouseMerchants, Product product)
    {
        DomainGuard.AgainstNull(product);
        DomainGuard.AgainstNullOrEmpty(warehouseMerchants);

        var activeWarehouseMerchants = warehouseMerchants.Where(wm => wm.Active);

        var activeAndValidWarehouseMerchants =
            activeWarehouseMerchants.Where(wm => product.GetOfferMerchants().Contains(wm.MerchantId));

        return activeAndValidWarehouseMerchants.ToList().AsReadOnly();
    }
}