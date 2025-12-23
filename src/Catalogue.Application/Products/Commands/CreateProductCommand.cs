using Catalogue.Domain.Common.Enums;
using Catalogue.Domain.Products;
using Catalogue.Domain.Products.ValueObjects;

namespace Catalogue.Application.Products.Commands;

public record CreateProductCommand(
    string ProductCode,
    string ProductTitle,
    string ShortDescription,
    int ManufacturerClassificationId,
    int ProductTypeId,
    int ProductSubTypeId,
    TaxClass TaxClass)
{
    public Product ToEntity()
    {
        var product = Product.Create(
            id: Domain.Products.ValueObjects.ProductCode.Create(ProductCode),
            productTitle: Domain.Products.ValueObjects.ProductTitle.Create(ProductTitle),
            description: ProductDescription.Create(ShortDescription),
            taxClass: TaxClass,
            productTypeId: Domain.ProductTypes.ValueObjects.ProductTypeId.Create(ProductTypeId),
            productSubTypeId: Domain.ProductTypes.ValueObjects.ProductSubTypeId.Create(ProductSubTypeId),
            manufacturerClassificationId: Domain.Lookups.ManufacturerClassifications.ValueObjects
                .ManufacturerClassificationId.Create(ManufacturerClassificationId)
        );

        return product;
    }
}