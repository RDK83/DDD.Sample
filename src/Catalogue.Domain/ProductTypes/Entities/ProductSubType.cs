using Catalogue.Domain.ProductTypes.ValueObjects;
using Shared.Domain.Aggregates.Entities;

namespace Catalogue.Domain.ProductTypes.Entities;

public class ProductSubType : BaseEntity<ProductSubTypeId>
{
    public ProductTypeId ProductTypeId { get; private set; }

    public ProductSubTypeName ProductSubTypeName { get; private set; }

    public bool Active { get; private set; }

    protected ProductSubType(ProductTypeId productTypeId, ProductSubTypeName productSubTypeName, bool active)
    {
        ProductTypeId = productTypeId;
        ProductSubTypeName = productSubTypeName;
        Active = active;
    }

    internal static ProductSubType Create(ProductTypeId productTypeId, ProductSubTypeName productSubTypeName, bool active)
    {
        return new ProductSubType(productTypeId, productSubTypeName, active);
    }
}