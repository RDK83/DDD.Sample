using Catalogue.Application.ProductTypes.Responses;
using Catalogue.Application.ProductTypes.Specifications.Helper;
using Catalogue.Application.Shared.Logging;
using Catalogue.Domain.ProductTypes.Repositories;
using Catalogue.Domain.ProductTypes.ValueObjects;

namespace Catalogue.Application.ProductTypes;

[LogMethod]
[LogParameters]
public class ProductTypeService(IProductTypeRepository repository) : IProductTypeService
{
    public async Task<ProductTypeDto> GetByIdAsync(int productTypeId)
    {
        var id = ProductTypeId.Create(productTypeId);

        var productType = await repository.GetFirstBySpecificationAsync(ProductTypeSpecs.ById(id));

        var dto = ProductTypeDto.FromEntity(productType);

        return dto;
    }
}