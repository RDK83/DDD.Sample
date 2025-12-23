using Ardalis.Specification;
using Catalogue.Domain.ProductTypes;
using Catalogue.Domain.ProductTypes.ValueObjects;

namespace Catalogue.Application.ProductTypes.Specifications.Helper;

internal static class ProductTypeSpecs
{
    public static ISpecification<ProductType> ById(ProductTypeId productTypeId) => new ProductTypeByIdSpec(productTypeId);
}