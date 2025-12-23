using Catalogue.Domain.Common.Enums;
using Catalogue.Domain.Common.ValueObjects;
using Catalogue.Domain.Common.ValueObjects.Address;
using Catalogue.Domain.Lookups.DeliveryMethods;
using Catalogue.Domain.Lookups.DeliveryMethods.ValueObjects;
using Catalogue.Domain.Lookups.ManufacturerClassifications;
using Catalogue.Domain.Lookups.ManufacturerClassifications.ValueObjects;
using Catalogue.Domain.Lookups.MediaTypes;
using Catalogue.Domain.Lookups.MediaTypes.ValueObjects;
using Catalogue.Domain.Medias;
using Catalogue.Domain.Medias.ValueObjects;
using Catalogue.Domain.Merchants;
using Catalogue.Domain.Merchants.ValueObjects;
using Catalogue.Domain.Products;
using Catalogue.Domain.Products.Mutations;
using Catalogue.Domain.Products.ValueObjects;
using Catalogue.Domain.ProductTypes;
using Catalogue.Domain.ProductTypes.ValueObjects;
using Catalogue.Domain.WarehouseMerchants;
using Catalogue.Domain.WarehouseMerchants.Policies;
using Catalogue.Domain.WarehouseMerchants.ValueObjects;
using Catalogue.Domain.Warehouses;
using Catalogue.Domain.Warehouses.Mutations;
using Catalogue.Domain.Warehouses.ValueObjects;
using Shared.Domain.Aggregates.Entities;

namespace Catalogue.Infrastructure.Persistence.Configurations;

public static class SeedData
{
    public static void Initialize(ApplicationDbContext db)
    {
        if (!db.Set<DeliveryMethod>().Any())
        {
            db.Set<DeliveryMethod>().AddRange(CreateDeliveryMethodLookups());
            db.SaveChanges();
        }

        if (!db.Set<ManufacturerClassification>().Any())
        {
            db.Set<ManufacturerClassification>().Add(CreateManClassLookup());
            db.SaveChanges();
        }

        if (!db.Set<MediaType>().Any())
        {
            db.Set<MediaType>().AddRange(CreateMediaTypeLookups());
            db.SaveChanges();
        }

        if (!db.Set<ProductType>().Any())
        {
            db.Set<ProductType>().Add(CreateProductType());
            db.SaveChanges();
        }

        if (!db.Set<Warehouse>().Any())
        {
            db.Set<Warehouse>().Add(CreateWarehouse());
            db.SaveChanges();
        }

        if (!db.Set<Merchant>().Any())
        {
            db.Set<Merchant>().AddRange(CreateMerchants());
            db.SaveChanges();
        }

        if (!db.Set<WarehouseMerchant>().Any())
        {
            db.Set<WarehouseMerchant>().AddRange(CreateWarehouseMerchants());
            db.SaveChanges();
        }

        if (!db.Set<Media>().Any())
        {
            db.Set<Media>().Add(CreateMedia());
            db.SaveChanges();
        }

        if (!db.Set<Product>().Any())
        {
            db.Set<Product>().Add(CreateProduct());
            db.SaveChanges();
        }
    }

    private static ManufacturerClassification CreateManClassLookup()
    {
        return ManufacturerClassification.Create("Manufacturer Description", true);
    }


    private static IEnumerable<DeliveryMethod> CreateDeliveryMethodLookups()
    {
        var deliveryMethod1 = DeliveryMethod.Create(
            DeliveryMethodId.Create("SD"),
            "Standard Delivery",
            true);

        var deliveryMethod2 = DeliveryMethod.Create(
            DeliveryMethodId.Create("XD"),
            "Express Delivery",
            true);

        return [deliveryMethod1, deliveryMethod2];
    }

    public static MediaType[] CreateMediaTypeLookups()
    {
        var mediaType1 = MediaType.Create("Picture");
        var mediaType2 = MediaType.Create("Video");

        return [mediaType1, mediaType2];
    }

    private static ProductType CreateProductType()
    {
        var productType = ProductType.Create(
            ProductTypeName.Create("Printer"),
            true);

        productType.AddProductSubType(
            ProductSubTypeName.Create("Laser"),
            true);

        return productType;
    }

    private static Warehouse CreateWarehouse()
    {
        var mutation = new NewWarehouseDeliveryMethodMutation(
            WarehouseId.Create(1),
            DeliveryMethodId.Create("SD"),
            Country.GB,
            Money.Create(Currency.GBP, 2.00m),
            WarehouseDeliveryLeadTime.Create(1, 3),
            true
        );

        var warehouse = Warehouse.Create(
            WarehouseName.Create("Warehouse One"),
            WarehouseCode.Create("WH1"),
            CreateAddress(),
            active: true
        );

        warehouse.AddDeliveryMethod(mutation);

        return warehouse;
    }


    private static Merchant[] CreateMerchants()
    {
        var merchantA = Merchant.Create(
            MerchantName.Create("Merchant A"),
            MerchantCode.Create("MERCHA"),
            CreateAddress(),
            true);

        var merchantB = Merchant.Create(
            MerchantName.Create("Merchant B"),
            MerchantCode.Create("MERCHB"),
            CreateAddress(),
            true);

        return [merchantA, merchantB];
    }

    private static WarehouseMerchant[] CreateWarehouseMerchants()
    {
        var policy = new WarehouseMerchantUniquePreferenceOrderPolicy([]);
        var warehouseMerchant1 = WarehouseMerchant.Create(
            WarehouseId.Create(1),
            MerchantId.Create(1),
            PreferenceOrder.Create(10),
            true,
            policy);

        var warehouseMerchant2 = WarehouseMerchant.Create(
            WarehouseId.Create(1),
            MerchantId.Create(2),
            PreferenceOrder.Create(20),
            true,
            policy);

        return [warehouseMerchant1, warehouseMerchant2];
    }

    private static Address CreateAddress()
    {
        return Address.Create(
            PrimaryAddressLine.Create("1 Some Location"),
            AddressLine.Create("Some Street"),
            AddressLine.Create(null),
            AddressLine.Create(null),
            City.Create("City"),
            County.Create("County"),
            PostalCode.Create(
                "B1 2PS", Country.GB)
        );
    }

    public static Media CreateMedia()
    {
        var media = Media.Create(
            MediaTypeId.Create(1),
            MediaUrl.Create("http://fakedomain.co.uk/pictures/1"),
            MediaAltText.Create("A lovely picture of a printer")
        );

        return media;
    }

    private static Product CreateProduct()
    {
        var mutation = new NewOfferMutation(
            ProductCode.Create("PROD1"),
            WarehouseId.Create(1),
            MerchantId.Create(1),
            Money.Create(19.99m),
            StockLevel.Create(20)
        );

        // 2nd Offer on Merchant(2) should be less prioritised
        var mutation2 = new NewOfferMutation(
            ProductCode.Create("PROD1"),
            WarehouseId.Create(1),
            MerchantId.Create(2),
            Money.Create(19.99m),
            StockLevel.Create(20)
        );

        var product = Product.Create(
            id: ProductCode.Create("PROD1"),
            productTitle: ProductTitle.Create("ProductTitle"),
            description: ProductDescription.Create("Description"),
            taxClass: TaxClass.Rated,
            productTypeId: ProductTypeId.Create(1),
            productSubTypeId: ProductSubTypeId.Create(1),
            manufacturerClassificationId: ManufacturerClassificationId.Create(1)
        );

        product.AddOffer(mutation);
        product.AddOffer(mutation2);

        product.AddMedia(MediaId.Create(1));

        //TODO interceptor doesn't seem to work for Seeding data so having to work around it for now
        ((IHasUpdatedTimeStamp)product).UpdatedAt = DateTime.UtcNow;

        return product;
    }
}