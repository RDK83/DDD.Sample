using Ardalis.Specification;
using Catalogue.Domain.ProductTypes.ValueObjects;

namespace Catalogue.Application.ProductTypes.Specifications;

internal class ProductTypeByIdSpec : BaseProductTypeSpec
{
    public ProductTypeByIdSpec(ProductTypeId productTypeId)
    {
        Query.Where(pt => pt.Id == productTypeId);
    }
}