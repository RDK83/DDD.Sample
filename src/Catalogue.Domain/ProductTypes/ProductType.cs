using Catalogue.Domain.ProductTypes.Entities;
using Catalogue.Domain.ProductTypes.ValueObjects;
using Shared.Domain.Aggregates;
using Shared.Domain.Exceptions;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

namespace Catalogue.Domain.ProductTypes;

public class ProductType : BaseAggregate<ProductTypeId>
{
    public ProductTypeName ProductTypeName { get; private set; }

    public IReadOnlyCollection<ProductSubType> ProductSubTypes => _productSubTypes;

    public bool Active { get; private set; }

    private readonly List<ProductSubType> _productSubTypes;

    private ProductType()
    {
        //EF
    }

    protected ProductType(ProductTypeName productTypeName, bool active)
    {
        ProductTypeName = productTypeName;
        Active = active;
        _productSubTypes = [];
    }

    public static ProductType Create(ProductTypeName productTypeName, bool active)
    {
        return new ProductType(productTypeName, active);
    }

    public void VerifySubTypeIsValid(ProductSubTypeId subTypeId)
    {
        if (!_productSubTypes.Any(x => x.Id.Equals(subTypeId)))
            throw new DomainValidationException(
                $"{nameof(ProductSubType)}: {subTypeId} not valid for given {nameof(ProductType)}: {Id}");
    }


    public void AddProductSubType(ProductSubTypeName productSubTypeName, bool active)
    {
        if (_productSubTypes.Any(sub => sub.ProductSubTypeName.Equals(productSubTypeName)))
            throw new ChildEntityAlreadyExistsException(nameof(ProductType), nameof(ProductSubType), Id.ToString(),
                productSubTypeName);

        var newSubType = ProductSubType.Create(Id, productSubTypeName, active);

        _productSubTypes.Add(newSubType);
    }
}