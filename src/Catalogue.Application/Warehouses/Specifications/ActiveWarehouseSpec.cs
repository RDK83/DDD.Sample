using Ardalis.Specification;

namespace Catalogue.Application.Warehouses.Specifications;

internal class ActiveWarehouseSpec : BaseWarehouseSpec
{
    public ActiveWarehouseSpec(bool active = true)
    {
        Query.Where(m => m.Active == active);
    }
}