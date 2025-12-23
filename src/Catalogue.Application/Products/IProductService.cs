using Catalogue.Application.Products.Commands;
using Catalogue.Application.Products.Commands.Medias;
using Catalogue.Application.Products.Commands.Offers;
using Catalogue.Application.Products.Queries;
using Catalogue.Application.Products.Responses;
using Shared.Application.Pagination;
using SharedKernel.Pagination;

namespace Catalogue.Application.Products;

public interface IProductService
{
    Task<ProductDto> GetByIdAsync(GetProductByIdQuery query);
    Task<OffsetPagedResult<ProductDto>> GetOffSetPagedAsync(ProductFilter filter, PagingQuery paging);
    Task<KeysetPagedResult<ProductDto, string>> GetKeysetPagedAsync(PagingQuery paging);
    Task<ProductDto> CreateAsync(CreateProductCommand product);
    Task UpdateAsync(UpdateProductCommand command);
    Task DeleteAsync(DeleteProductCommand command);

    Task CreateProductOfferAsync(CreateOfferCommand command);
    Task UpdateProductOfferAsync(UpdateOfferCommand command);
    Task DeleteProductOfferAsync(DeleteOfferCommand command);

    Task CreateProductMediaAsync(CreateProductMediaCommand command);
}