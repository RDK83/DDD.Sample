using Catalogue.Domain.ProductTypes;
using Catalogue.Domain.ProductTypes.ValueObjects;
using Catalogue.Domain.UnitTests.Common;

namespace Catalogue.Domain.UnitTests.ProductTypes;

internal static class ProductTypesTestHelper
{
    public static ProductTypeId ValidProductTypeId() => ProductTypeId.Create(1);
    public static ProductSubTypeId ValidProductSubTypeId() => ProductSubTypeId.Create(1);

    public static ProductSubTypeName ValidProductSubTypeName() => ProductSubTypeName.Create("SubType");


    public static ProductType CreateBaseProductType()
    {
        var productType = ProductType.Create(
            ProductTypeName.Create("Test"),
            true);

        return productType;
    }

    public static ProductType CreateProductTypeWithSubTypeAndAttributeMapping()
    {
        var productType = CreateBaseProductType();

        var fakeId = ProductTypeId.Create(1);
        TestReflectionHelper.SetAutoPropertyBackingField(productType, nameof(productType.Id), fakeId);

        productType.AddProductSubType(
            ValidProductSubTypeName(),
            true);

        var subType = productType.ProductSubTypes.First();

        TestReflectionHelper.SetAutoPropertyBackingField(subType, nameof(subType.Id), ValidProductSubTypeId());

        return productType;
    }
}