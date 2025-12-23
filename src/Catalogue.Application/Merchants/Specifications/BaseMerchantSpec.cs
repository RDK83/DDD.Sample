using Ardalis.Specification;
using Catalogue.Domain.Merchants;

namespace Catalogue.Application.Merchants.Specifications;

internal abstract class BaseMerchantSpec : Specification<Merchant>
{
    protected BaseMerchantSpec()
    {
    }
}