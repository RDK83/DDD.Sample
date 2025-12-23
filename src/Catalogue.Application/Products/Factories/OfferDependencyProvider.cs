using Catalogue.Application.Merchants.Specifications.Helper;
using Catalogue.Application.Products.Contexts;
using Catalogue.Application.WarehouseMerchants.Specifications.Helper;
using Catalogue.Application.Warehouses.Specifications.Helper;
using Catalogue.Domain.Merchants.Repositories;
using Catalogue.Domain.WarehouseMerchants;
using Catalogue.Domain.Warehouses.Repositories;

namespace Catalogue.Application.Products.Factories;

public class OfferDependencyProvider(
    IMerchantRepository merchantRepository,
    IWarehouseMerchantRepository warehouseMerchantRepository,
    IWarehouseRepository warehouseRepository) : IOfferDependencyProvider
{
    public async Task<OfferDependencies> LoadAsync()
    {
        var activeMerchants = await merchantRepository.GetManyBySpecificationAsync(MerchantSpecs.Active());
        var warehouses = await warehouseRepository.GetManyBySpecificationAsync(WarehouseSpecs.Active());
        var activeWarehouseMerchants =
            await warehouseMerchantRepository.GetManyBySpecificationAsync(WarehouseMerchantSpecs.Active());

        return new OfferDependencies
        (
            activeMerchants,
            warehouses,
            activeWarehouseMerchants
        );
    }
}