using Ardalis.Specification;
using Catalogue.Domain.ProductTypes;

namespace Catalogue.Application.ProductTypes.Specifications;

internal class BaseProductTypeSpec : Specification<ProductType>
{
    public BaseProductTypeSpec()
    {
        Query
            .Include(pt => pt.ProductSubTypes)
            .AsSplitQuery();
    }
}