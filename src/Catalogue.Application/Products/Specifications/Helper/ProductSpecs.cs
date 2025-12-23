using Ardalis.Specification;
using Catalogue.Domain.Products;
using Catalogue.Domain.Products.ValueObjects;

namespace Catalogue.Application.Products.Specifications.Helper;

internal static class ProductSpecs
{
    internal static ISpecification<Product> ById(ProductCode productCode) => new ProductByIdSpec(productCode);
}