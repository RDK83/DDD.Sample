using Ardalis.Specification;
using Catalogue.Domain.Products;

namespace Catalogue.Application.Products.Specifications;

internal class BaseProductSpec : Specification<Product>
{
    internal BaseProductSpec()
    {
        Query
            .Include(p => p.Offers)
            .Include(p => p.ProductMedias)
            .Include(p => p.ManufacturerClassification)
            .AsSplitQuery();
    }
}