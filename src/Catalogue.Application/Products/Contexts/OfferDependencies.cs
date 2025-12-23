using Catalogue.Domain.Merchants;
using Catalogue.Domain.WarehouseMerchants;
using Catalogue.Domain.Warehouses;

namespace Catalogue.Application.Products.Contexts;

public record OfferDependencies(
    IReadOnlyCollection<Merchant> ActiveMerchants,
    IReadOnlyCollection<Warehouse> ActiveWarehouses,
    IReadOnlyCollection<WarehouseMerchant> ActiveWarehouseMerchants
);