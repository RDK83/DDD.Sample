using Catalogue.Domain.ProductTypes;
using Catalogue.Domain.ProductTypes.ValueObjects;
using Shared.Domain.Exceptions;

namespace Catalogue.Domain.UnitTests.ProductTypes;

internal class ProductTypeTests
{
    [Test]
    public void Create_WithValidInput_ShouldCreateSuccessfully()
    {
        var productTypeName = ProductTypeName.Create("test");

        var productType = ProductType.Create(productTypeName, true);

        Assert.Multiple(() =>
        {
            Assert.That(productType.ProductTypeName, Is.EqualTo(productTypeName));
            Assert.That(productType.Active, Is.EqualTo(true));
        });
    }

    [Test]
    public void VerifySubTypeIsValid_WithValidInput_ShouldNotThrow()
    {
        var productType = ProductTypesTestHelper.CreateProductTypeWithSubTypeAndAttributeMapping();

        productType.VerifySubTypeIsValid(ProductTypesTestHelper.ValidProductSubTypeId());
    }

    [Test]
    public void VerifySubTypeIsValid_WithInvalidInput_ShouldThrow()
    {
        var productType = ProductTypesTestHelper.CreateProductTypeWithSubTypeAndAttributeMapping();

        Assert.Throws<DomainValidationException>(() => productType.VerifySubTypeIsValid(ProductSubTypeId.Create(9)));
    }


    [Test]
    public void AddProductSubType_WithValidInput_ShouldAddSuccessfully()
    {
        var productType = ProductTypesTestHelper.CreateBaseProductType();

        productType.AddProductSubType(ProductTypesTestHelper.ValidProductSubTypeName(), true);

        Assert.Multiple(() =>
        {
            Assert.That(productType.ProductSubTypes.Count, Is.EqualTo(1));
            Assert.That(productType.ProductSubTypes.First().ProductSubTypeName,
                Is.EqualTo(ProductTypesTestHelper.ValidProductSubTypeName()));
        });
    }

    [Test]
    public void AddProductSubType_WithDuplicateInput_ShouldThrow()
    {
        var productType = ProductTypesTestHelper.CreateProductTypeWithSubTypeAndAttributeMapping();

        Assert.Throws<ChildEntityAlreadyExistsException>(() =>
            productType.AddProductSubType(ProductTypesTestHelper.ValidProductSubTypeName(), true));
    }
}