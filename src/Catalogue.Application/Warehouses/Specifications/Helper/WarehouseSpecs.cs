using Ardalis.Specification;
using Catalogue.Domain.Warehouses;
using Catalogue.Domain.Warehouses.ValueObjects;

namespace Catalogue.Application.Warehouses.Specifications.Helper;

internal static class WarehouseSpecs
{
    public static ISpecification<Warehouse> ById(WarehouseId warehouseId) => new WarehouseByIdSpec(warehouseId);
    public static ISpecification<Warehouse> All() => new BaseWarehouseSpec();
    public static ISpecification<Warehouse> Active(bool active = true) => new ActiveWarehouseSpec(active);
}