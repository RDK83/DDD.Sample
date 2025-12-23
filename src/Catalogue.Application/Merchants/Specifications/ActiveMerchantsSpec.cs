using Ardalis.Specification;

namespace Catalogue.Application.Merchants.Specifications;

internal class ActiveMerchantsSpec : BaseMerchantSpec
{
    internal ActiveMerchantsSpec(bool active = true)
    {
        Query.Where(m => m.Active == active);
    }
}