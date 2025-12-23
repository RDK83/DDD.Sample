using Catalogue.Domain.ProductTypes.Entities;

namespace Catalogue.Application.ProductTypes.Responses;

public record ProductSubTypeDto(int ProductTypeId, string ProductSubTypeName, bool Active)
{
    public static ProductSubTypeDto FromEntity(ProductSubType entity)
    {
        return new ProductSubTypeDto(
            ProductTypeId: entity.ProductTypeId,
            ProductSubTypeName: entity.ProductSubTypeName,
            Active: entity.Active
        );
    }
}