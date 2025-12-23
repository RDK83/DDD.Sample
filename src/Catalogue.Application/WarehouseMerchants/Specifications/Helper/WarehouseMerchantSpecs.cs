using Ardalis.Specification;
using Catalogue.Domain.WarehouseMerchants;
using Catalogue.Domain.WarehouseMerchants.ValueObjects;

namespace Catalogue.Application.WarehouseMerchants.Specifications.Helper;

internal static class WarehouseMerchantSpecs
{
    public static ISpecification<WarehouseMerchant> Active(bool active = true) => new ActiveWarehouseMerchantsSpec(active);
    public static ISpecification<WarehouseMerchant> All() => new AllWarehouseMerchantSpec();
    public static ISpecification<WarehouseMerchant> ById(WarehouseMerchantId id) => new WarehouseMerchantByIdSpec(id);
}