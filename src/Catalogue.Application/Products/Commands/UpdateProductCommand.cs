using Catalogue.Domain.Common.Enums;
using Catalogue.Domain.Products.Mutations;
using Catalogue.Domain.Products.ValueObjects;

namespace Catalogue.Application.Products.Commands;

public record UpdateProductCommand(
    string ProductCode,
    string ProductTitle,
    string ShortDescription,
    int ManufacturerClassificationId,
    int ProductTypeId,
    int ProductSubTypeId,
    TaxClass TaxClass)
{
    public EditProductMutation ToMutation()
    {
        var mutation = new EditProductMutation(
            TaxClass: TaxClass,
            ProductTypeId: Domain.ProductTypes.ValueObjects.ProductTypeId.Create(ProductTypeId),
            ProductSubTypeId: Domain.ProductTypes.ValueObjects.ProductSubTypeId.Create(ProductSubTypeId),
            ProductTitle: Domain.Products.ValueObjects.ProductTitle.Create(ProductTitle),
            Description: ProductDescription.Create(ShortDescription),
            ManufacturerClassificationId: Domain.Lookups.ManufacturerClassifications.ValueObjects
                .ManufacturerClassificationId.Create(ManufacturerClassificationId)
        );

        return mutation;
    }
}