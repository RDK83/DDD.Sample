using Ardalis.Specification;
using Catalogue.Domain.WarehouseMerchants.ValueObjects;

namespace Catalogue.Application.WarehouseMerchants.Specifications;

internal class WarehouseMerchantByIdSpec : BaseWarehouseMerchantSpec
{
    public WarehouseMerchantByIdSpec(WarehouseMerchantId warehouseMerchantId)
    {
        Query.Where(wm => wm.Id.Equals(warehouseMerchantId));
    }
}