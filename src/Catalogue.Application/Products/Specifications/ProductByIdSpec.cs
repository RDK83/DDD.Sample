using Ardalis.Specification;
using Catalogue.Domain.Products.ValueObjects;

namespace Catalogue.Application.Products.Specifications;

internal class ProductByIdSpec : BaseProductSpec
{
    internal ProductByIdSpec(ProductCode productCode)
    {
        Query.Where(p => p.Id.Equals(productCode));
    }
}