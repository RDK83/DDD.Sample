using System.Linq.Expressions;
using Ardalis.Specification;
using Catalogue.Domain.Products;
using Catalogue.Domain.Products.ValueObjects;
using SharedKernel.Pagination;

namespace Catalogue.Application.Products.Specifications;

internal class ProductKeysetPagedSpec : BaseProductSpec, IKeysetPagedSpecification<Product, ProductCode>
{
    public Expression<Func<Product, ProductCode>> KeySelector => p => p.Id;

    public ProductKeysetPagedSpec(ProductCode? lastCode, bool descending = false)
    {
        if (lastCode is not null)
        {
            Query.Where(p => descending
                ? string.Compare(p.Id, lastCode) < 0
                : string.Compare(p.Id, lastCode) > 0);
        }

        if (descending)
            Query.OrderByDescending(p => p.Id);
        else
            Query.OrderBy(p => p.Id);
    }
}