using Ardalis.Specification;
using Catalogue.Application.Products.Queries;
using Catalogue.Domain.ProductTypes.ValueObjects;

namespace Catalogue.Application.Products.Specifications;

internal class ProductByFilterSpec : BaseProductSpec
{
    internal ProductByFilterSpec(ProductFilter filter)
    {
        if (filter.ProductTypeId != null)
        {
            var filterProductTypeId = ProductTypeId.Create((int)filter.ProductTypeId);
            Query.Where(p => p.ProductTypeId.Equals(filterProductTypeId));
        }

        if (filter.InStock != null)
        {
            if (filter.InStock.Value)
                Query.Where(p => p.Offers.Any(o => o.StockLevel > 0));
            else
                Query.Where(p => p.Offers.All(o => o.StockLevel <= 0));
        }

        Query.OrderBy(p => p.Id);
    }
}