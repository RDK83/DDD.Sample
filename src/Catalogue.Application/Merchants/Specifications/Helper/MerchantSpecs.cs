using Ardalis.Specification;
using Catalogue.Domain.Merchants;
using Catalogue.Domain.Merchants.ValueObjects;

namespace Catalogue.Application.Merchants.Specifications.Helper;

internal static class MerchantSpecs
{
    internal static ISpecification<Merchant> Active(bool active = true) => new ActiveMerchantsSpec(active);
    internal static ISpecification<Merchant> ById(MerchantId id) => new MerchantByIdSpec(id);
}