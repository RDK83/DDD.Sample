using Catalogue.Domain.ProductTypes;

namespace Catalogue.Application.ProductTypes.Responses;

public record ProductTypeDto(
    string ProductTypeName,
    bool Active,
    IReadOnlyCollection<ProductSubTypeDto> ProductSubTypes
)
{
    public static ProductTypeDto FromEntity(ProductType entity)
    {
        return new ProductTypeDto(
            ProductTypeName: entity.ProductTypeName,
            Active: entity.Active,
            ProductSubTypes: entity.ProductSubTypes.Select(ProductSubTypeDto.FromEntity).ToList()
        );
    }
}