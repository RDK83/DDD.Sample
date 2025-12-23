using Catalogue.Domain.Products.Entities;

namespace Catalogue.Application.Products.Responses;

public record ProductMediaDto(string ProductCode, int MediaId)
{
    public static ProductMediaDto FromEntity(ProductMedia entity)
    {
        var dto = new ProductMediaDto(
            ProductCode: entity.ProductCode,
            MediaId: entity.MediaId);

        return dto;
    }
}