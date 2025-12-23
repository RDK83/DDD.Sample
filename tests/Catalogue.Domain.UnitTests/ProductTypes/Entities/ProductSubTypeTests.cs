using Catalogue.Domain.ProductTypes.Entities;
using Catalogue.Domain.ProductTypes.ValueObjects;

namespace Catalogue.Domain.UnitTests.ProductTypes.Entities;

internal class ProductSubTypeTests
{
    [Test]
    public void Create_WithValidInput_ShouldCreateSuccessfully()
    {
        var productTypeId = ProductTypeId.Create(1);
        var productSubTypeName = ProductSubTypeName.Create("test");

        var subType = ProductSubType.Create(productTypeId, productSubTypeName, true);

        Assert.Multiple(() =>
        {
            Assert.That(subType.ProductSubTypeName, Is.EqualTo(productSubTypeName));
            Assert.That(subType.Active, Is.EqualTo(true));
        });
    }
}