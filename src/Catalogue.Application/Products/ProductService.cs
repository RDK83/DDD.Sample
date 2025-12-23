using Catalogue.Application.Medias.Specifications.Helper;
using Catalogue.Application.Merchants.Specifications.Helper;
using Catalogue.Application.Products.Commands;
using Catalogue.Application.Products.Commands.Medias;
using Catalogue.Application.Products.Commands.Offers;
using Catalogue.Application.Products.Factories;
using Catalogue.Application.Products.Queries;
using Catalogue.Application.Products.Responses;
using Catalogue.Application.Products.Specifications;
using Catalogue.Application.Products.Specifications.Helper;
using Catalogue.Application.ProductTypes.Specifications.Helper;
using Catalogue.Application.Shared.Logging;
using Catalogue.Application.Warehouses.Specifications.Helper;
using Catalogue.Domain.Lookups.ManufacturerClassifications;
using Catalogue.Domain.Lookups.ManufacturerClassifications.ValueObjects;
using Catalogue.Domain.Medias.Repositories;
using Catalogue.Domain.Medias.ValueObjects;
using Catalogue.Domain.Merchants.Repositories;
using Catalogue.Domain.Merchants.ValueObjects;
using Catalogue.Domain.Products.Events;
using Catalogue.Domain.Products.Repositories;
using Catalogue.Domain.Products.ValueObjects;
using Catalogue.Domain.ProductTypes.Repositories;
using Catalogue.Domain.ProductTypes.ValueObjects;
using Catalogue.Domain.WarehouseMerchants.Policies;
using Catalogue.Domain.Warehouses.Repositories;
using Catalogue.Domain.Warehouses.ValueObjects;
using Shared.Application.Pagination;
using Shared.Application.UnitOfWork;
using SharedKernel.Pagination;
using SharedKernel.Repositories;

namespace Catalogue.Application.Products;

[LogMethod]
[LogParameters]
public class ProductService(
    IProductRepository productRepository,
    IOfferDependencyProvider offerDependencyProvider,
    IUnitOfWork unitOfWork,
    IMerchantReadRepository merchantReadRepository,
    IWarehouseReadRepository warehouseReadRepository,
    IProductTypeReadRepository productTypeReadRepository,
    IMediaReadRepository mediaReadRepository,
    ILookupReadRepository<ManufacturerClassification, ManufacturerClassificationId> manClassLookupReadRepository)
    : IProductService
{
    public async Task<ProductDto> GetByIdAsync(GetProductByIdQuery query)
    {
        var productCode = ProductCode.Create(query.ProductCode);
        var product = await productRepository.GetFirstBySpecificationAsync(ProductSpecs.ById(productCode));

        var offerDependencies = await offerDependencyProvider.LoadAsync();

        var context = ViableOfferCalculationContextFactory.Build(product, offerDependencies);
        product.SetPromotedOffer(context, new PreferredWarehouseMerchantPolicy());

        return ProductDto.FromEntity(product);
    }

    public async Task<OffsetPagedResult<ProductDto>> GetOffSetPagedAsync(ProductFilter filter, PagingQuery paging)
    {
        var spec = new ProductByFilterSpec(filter);

        var pagedResult =
            await productRepository.GetOffsetPagedBySpecificationAsync(spec, paging.PageNumber, paging.PageSize);

        var offerDependencies = await offerDependencyProvider.LoadAsync();

        foreach (var product in pagedResult.Items)
        {
            var context = ViableOfferCalculationContextFactory.Build(product, offerDependencies);

            var preferredWarehouseMerchantPolicy = new PreferredWarehouseMerchantPolicy();
            product.SetPromotedOffer(context, preferredWarehouseMerchantPolicy);
        }

        var products = pagedResult.Items.Select(ProductDto.FromEntity).ToList().AsReadOnly();

        return new OffsetPagedResult<ProductDto>(products, pagedResult.TotalCount, pagedResult.PageNumber,
            pagedResult.PageSize);
    }

    public async Task<KeysetPagedResult<ProductDto, string>> GetKeysetPagedAsync(PagingQuery paging)
    {
        if (!string.IsNullOrWhiteSpace(paging.Cursor))
            await productRepository.VerifyExistsAsync(ProductSpecs.ById(ProductCode.Create(paging.Cursor)));

        var spec = new ProductKeysetPagedSpec(
            paging.Cursor is not null ? ProductCode.Create(paging.Cursor) : null,
            paging.Descending);

        var pagedResult = await productRepository.GetKeysetPagedAsync(
            spec,
            paging.PageSize);

        var offerDependencies = await offerDependencyProvider.LoadAsync();

        foreach (var product in pagedResult.Items)
        {
            var context = ViableOfferCalculationContextFactory.Build(product, offerDependencies);

            var preferredWarehouseMerchantPolicy = new PreferredWarehouseMerchantPolicy();
            product.SetPromotedOffer(context, preferredWarehouseMerchantPolicy);
        }

        var products = pagedResult.Items.Select(ProductDto.FromEntity).ToList().AsReadOnly();

        // Determine the NextKey for the cursor
        var nextKey = pagedResult.LastKey?.Value;

        return new KeysetPagedResult<ProductDto, string>(products, nextKey);
    }

    public async Task<ProductDto> CreateAsync(CreateProductCommand command)
    {
        var productTypeId = ProductTypeId.Create(command.ProductTypeId);
        var productType =
            await productTypeReadRepository.GetFirstBySpecificationAsync(ProductTypeSpecs.ById(productTypeId));
        productType.VerifySubTypeIsValid(ProductSubTypeId.Create(command.ProductSubTypeId));

        var manClassId = ManufacturerClassificationId.Create(command.ManufacturerClassificationId);
        await manClassLookupReadRepository.VerifyExistsAsync(manClassId);

        var newProduct = command.ToEntity();
        productRepository.Add(newProduct);

        await unitOfWork.SaveChangesAsync();

        var createdProduct =
            await productRepository.GetFirstBySpecificationAsync(ProductSpecs.ById(newProduct.Id));
        return ProductDto.FromEntity(createdProduct);
    }

    public async Task UpdateAsync(UpdateProductCommand command)
    {
        var productTypeId = ProductTypeId.Create(command.ProductTypeId);
        var productType =
            await productTypeReadRepository.GetFirstBySpecificationAsync(ProductTypeSpecs.ById(productTypeId));
        productType.VerifySubTypeIsValid(ProductSubTypeId.Create(command.ProductSubTypeId));

        var manClassId = ManufacturerClassificationId.Create(command.ManufacturerClassificationId);
        await manClassLookupReadRepository.VerifyExistsAsync(manClassId);

        var productCode = ProductCode.Create(command.ProductCode);
        var existingProduct = await productRepository.GetFirstBySpecificationAsync(ProductSpecs.ById(productCode));

        var editValues = command.ToMutation();
        existingProduct.UpdateDetails(editValues);

        await unitOfWork.SaveChangesAsync();
    }

    public async Task DeleteAsync(DeleteProductCommand command)
    {
        var productCode = ProductCode.Create(command.ProductCode);
        var deleteCandidate = await productRepository.GetFirstBySpecificationAsync(ProductSpecs.ById(productCode));

        var mediaIds = deleteCandidate.ProductMedias.Select(pm => pm.MediaId).ToList().AsReadOnly();
        deleteCandidate.AddDomainEvent(new ProductDeletedEvent(deleteCandidate.Id, mediaIds));
        //deleteCandidate.AddDomainEvent(new EventualProductDeletedEvent(deleteCandidate.VersionName, deleteCandidate.ProductCode, mediaIds));

        productRepository.Remove(deleteCandidate);

        await unitOfWork.SaveChangesAsync();
    }

    public async Task CreateProductOfferAsync(CreateOfferCommand command)
    {
        await warehouseReadRepository.VerifyExistsAsync(WarehouseSpecs.ById(WarehouseId.Create(command.WarehouseId)));
        await merchantReadRepository.VerifyExistsAsync(MerchantSpecs.ById(MerchantId.Create(command.MerchantId)));

        var productCode = ProductCode.Create(command.ProductCode);
        var existingProduct = await productRepository.GetFirstBySpecificationAsync(ProductSpecs.ById(productCode));

        var newOfferMutation = command.ToMutation();
        existingProduct.AddOffer(newOfferMutation);

        await unitOfWork.SaveChangesAsync();
    }

    public async Task UpdateProductOfferAsync(UpdateOfferCommand command)
    {
        var productCode = ProductCode.Create(command.ProductCode);
        var product = await productRepository.GetFirstBySpecificationAsync(ProductSpecs.ById(productCode));

        var editOfferMutation = command.ToMutation();
        var offerId = OfferId.Create(command.Id);
        product.UpdateOffer(offerId, editOfferMutation);

        await unitOfWork.SaveChangesAsync();
    }

    public async Task DeleteProductOfferAsync(DeleteOfferCommand command)
    {
        var productCode = ProductCode.Create(command.ProductCode);
        var product = await productRepository.GetFirstBySpecificationAsync(ProductSpecs.ById(productCode));

        var offerId = OfferId.Create(command.Id);
        product.RemoveOffer(offerId);

        await unitOfWork.SaveChangesAsync();
    }

    public async Task CreateProductMediaAsync(CreateProductMediaCommand command)
    {
        var mediaId = MediaId.Create(command.MediaId);
        await mediaReadRepository.VerifyExistsAsync(MediaSpecs.ById(mediaId));

        var productCode = ProductCode.Create(command.ProductCode);
        var existingProduct = await productRepository.GetFirstBySpecificationAsync(ProductSpecs.ById(productCode));

        existingProduct.AddMedia(mediaId);

        await unitOfWork.SaveChangesAsync();
    }
}