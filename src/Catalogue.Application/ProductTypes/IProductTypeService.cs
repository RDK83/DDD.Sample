using Catalogue.Application.ProductTypes.Responses;

namespace Catalogue.Application.ProductTypes;

public interface IProductTypeService
{
    Task<ProductTypeDto> GetByIdAsync(int productTypeId);
}