using Catalogue.Domain.Common.Enums;
using Catalogue.Domain.Products;

namespace Catalogue.Application.Products.Responses;

public record ProductDto(
    string ProductCode,
    string ManufacturerClassification,
    string ProductTitle,
    string Description,
    TaxClass TaxClass,
    int ManufacturerClassificationId,
    int ProductSubTypeId,
    int ProductTypeId,
    int? PromotedOfferId,
    DateTime LastUpdated,
    IReadOnlyCollection<OfferDto> Offers,
    IReadOnlyCollection<ProductMediaDto> Medias)
{
    public static ProductDto FromEntity(Product product)
    {
        var dto = new ProductDto(
            ProductCode: product.Id,
            ManufacturerClassification: product.ManufacturerClassification.Description,
            ProductTitle: product.ProductTitle,
            Description: product.Description,
            TaxClass: product.TaxClass,
            ManufacturerClassificationId: product.ManufacturerClassificationId,
            ProductSubTypeId: product.ProductSubTypeId,
            ProductTypeId: product.ProductTypeId,
            PromotedOfferId: product.PromotedOfferId,
            LastUpdated: product.UpdatedAt,
            Offers: product.Offers.Select(OfferDto.FromEntity).ToList(),
            Medias: product.ProductMedias.Select(ProductMediaDto.FromEntity).ToList()
        );

        return dto;
    }
}