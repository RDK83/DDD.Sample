using Ardalis.Specification;
using Catalogue.Domain.WarehouseMerchants;

namespace Catalogue.Application.WarehouseMerchants.Specifications;

internal class ActiveWarehouseMerchantsSpec : Specification<WarehouseMerchant>
{
    public ActiveWarehouseMerchantsSpec(bool active = true)
    {
        Query.Where(wm => wm.Active == active);
    }
}