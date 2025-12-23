using Catalogue.Domain.Common.Enums;
using Catalogue.Domain.Common.ValueObjects;
using Catalogue.Domain.Lookups.ManufacturerClassifications.ValueObjects;
using Catalogue.Domain.Merchants.ValueObjects;
using Catalogue.Domain.Products;
using Catalogue.Domain.Products.Contexts;
using Catalogue.Domain.Products.Entities;
using Catalogue.Domain.Products.Mutations;
using Catalogue.Domain.Products.ValueObjects;
using Catalogue.Domain.ProductTypes.ValueObjects;
using Catalogue.Domain.WarehouseMerchants.ValueObjects;
using Catalogue.Domain.Warehouses.ValueObjects;

namespace Catalogue.Domain.UnitTests.Products;

public class ProductTestHelper
{
    public static MerchantId ValidMerchantId(int id = 123) =>
        MerchantId.Create(id);

    public static WarehouseId ValidWarehouseId() =>
        WarehouseId.Create(1);

    public static ProductCode ValidProductCode() => ProductCode.Create("TEST123");


    public static WarehouseMerchantStatus ValidWarehouseMerchantStatus(WarehouseId? warehouseId = null,
        MerchantId? merchantId = null, int preference = 1) =>
        WarehouseMerchantStatus.Create(
            warehouseId ?? ValidWarehouseId(),
            merchantId ?? ValidMerchantId(),
            PreferenceOrder.Create(preference),
            true);

    public static Product CreateBaseProduct()
    {
        return Product.Create(
            id: ValidProductCode(),
            productTitle: ProductTitle.Create("ProductTitle"),
            description: ProductDescription.Create("ShortDesc"),
            taxClass: TaxClass.Rated,
            productTypeId: ProductTypeId.Create(1),
            productSubTypeId: ProductSubTypeId.Create(1),
            manufacturerClassificationId: ManufacturerClassificationId.Create(1)
        );
    }

    public static Offer CreateBaseOffer(bool active = true, StockLevel? stockLevel = null)
    {
        return Offer.Create(
            ValidProductCode(),
            WarehouseId.Create(1),
            MerchantId.Create(1),
            Money.Create(19.99m),
            stockLevel ?? StockLevel.Create(42)
        );
    }

    public static EditProductMutation CreateEditProductMutation()
    {
        var mutation = new EditProductMutation(
            ProductTitle: ProductTitle.Create("AltProductTitle"),
            Description: ProductDescription.Create("AltDescription"),
            TaxClass: TaxClass.Exempt,
            ProductTypeId: ProductTypeId.Create(2),
            ProductSubTypeId: ProductSubTypeId.Create(2),
            ManufacturerClassificationId: ManufacturerClassificationId.Create(2)
        );

        return mutation;
    }


    public static NewOfferMutation CreateNewOfferMutation(Product product, int warehouseId = 1, int merchantId = 1)
    {
        var newOfferValues = new NewOfferMutation(
            ProductCode: product.Id,
            WarehouseId: WarehouseId.Create(warehouseId),
            MerchantId: MerchantId.Create(merchantId),
            GrossPrice: Money.Create(10),
            StockLevel: StockLevel.Create(10)
        );

        return newOfferValues;
    }

    public static EditOfferMutation CreateEditOfferMutation(Product product, int warehouseId = 1, int merchantId = 1)
    {
        var newOfferValues = new EditOfferMutation(
            GrossPrice: Money.Create(100m),
            StockLevel: StockLevel.Create(10)
        );

        return newOfferValues;
    }


    public static ViableOfferCalculationContext CreateViableOfferCalculationContext(
        IReadOnlyCollection<MerchantId>? merchants = null,
        IReadOnlyCollection<WarehouseId>? warehouses = null,
        IReadOnlyCollection<WarehouseMerchantStatus>? warehouseMerchants = null,
        bool availableForDisplay = true)
    {
        merchants ??=
        [
            MerchantId.Create(1),
            MerchantId.Create(2)
        ];
        warehouses ??=
        [
            WarehouseId.Create(1),
            WarehouseId.Create(2)
        ];
        warehouseMerchants ??=
        [
            WarehouseMerchantStatus.Create(WarehouseId.Create(1), MerchantId.Create(1), PreferenceOrder.Create(10),
                true),
            WarehouseMerchantStatus.Create(WarehouseId.Create(1), MerchantId.Create(2), PreferenceOrder.Create(20),
                true)
        ];

        return ViableOfferCalculationContext.Create(
            merchants,
            warehouses,
            warehouseMerchants
        );
    }
}