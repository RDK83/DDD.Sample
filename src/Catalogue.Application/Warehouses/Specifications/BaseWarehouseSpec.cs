using Ardalis.Specification;
using Catalogue.Domain.Warehouses;

namespace Catalogue.Application.Warehouses.Specifications;

internal class BaseWarehouseSpec : Specification<Warehouse>
{
    public BaseWarehouseSpec()
    {
        Query
            .Include(w => w.WarehouseDeliveryMethods)
            .ThenInclude(wdm => wdm.DeliveryMethod)
            .AsSplitQuery();
    }
}