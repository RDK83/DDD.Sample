using Ardalis.Specification;
using Catalogue.Domain.Warehouses.ValueObjects;

namespace Catalogue.Application.Warehouses.Specifications;

internal class WarehouseByIdSpec : BaseWarehouseSpec
{
    public WarehouseByIdSpec(WarehouseId warehouseId)
    {
        Query.Where(w => w.Id.Equals(warehouseId));
    }
}