using Catalogue.Domain.Common.ValueObjects;
using Catalogue.Domain.Merchants.ValueObjects;
using Catalogue.Domain.Products.ValueObjects;
using Catalogue.Domain.Warehouses.ValueObjects;

namespace Catalogue.Domain.Products.Mutations;

public record NewOfferMutation(
    ProductCode ProductCode,
    WarehouseId WarehouseId,
    MerchantId MerchantId,
    Money GrossPrice,
    StockLevel StockLevel);