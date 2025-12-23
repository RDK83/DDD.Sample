using Ardalis.Specification;
using Catalogue.Domain.Merchants.ValueObjects;

namespace Catalogue.Application.Merchants.Specifications;

internal class MerchantByIdSpec : BaseMerchantSpec
{
    internal MerchantByIdSpec(MerchantId id)
    {
        Query.Where(m => m.Id == id);
    }
}