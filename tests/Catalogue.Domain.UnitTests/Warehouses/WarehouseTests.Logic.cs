using Catalogue.Domain.UnitTests.Common;
using Catalogue.Domain.Warehouses.ValueObjects;

namespace Catalogue.Domain.UnitTests.Warehouses;

internal partial class WarehouseTests
{
    [Test]
    public void IsAvailable_WhenAllCriteriaSatisfied_ShouldReturnTrue()
    {
        var warehouse = WarehouseTestHelper.CreateWarehouse(active: true);

        var newDeliveryMethodMutation = WarehouseTestHelper.CreateNewWarehouseDeliveryMethodMutation(active: true);

        warehouse.AddDeliveryMethod(newDeliveryMethodMutation);

        var deliveryMethod = warehouse.WarehouseDeliveryMethods.First();
        var fakeWarehouseDeliveryMethodId = WarehouseDeliveryMethodId.Create(1);
        TestReflectionHelper.SetAutoPropertyBackingField(deliveryMethod, nameof(deliveryMethod.Id),
            fakeWarehouseDeliveryMethodId);

        var result = warehouse.IsAvailable();

        Assert.That(result, Is.True);
    }

    [Test]
    public void IsAvailable_WhenNotActive_ShouldReturnFalse()
    {
        var warehouse = WarehouseTestHelper.CreateWarehouse(active: false);

        var result = warehouse.IsAvailable();

        Assert.That(result, Is.False);
    }

    [Test]
    public void IsAvailable_WhenHasNoDeliveryMethods_ShouldReturnFalse()
    {
        var warehouse = WarehouseTestHelper.CreateWarehouse(active: true);

        var result = warehouse.IsAvailable();

        Assert.That(result, Is.False);
    }

    [Test]
    public void IsAvailable_WhenHasNoActiveDeliveryMethods_ShouldReturnFalse()
    {
        var warehouse = WarehouseTestHelper.CreateWarehouse(active: true);

        var newDeliveryMethodMutation = WarehouseTestHelper.CreateNewWarehouseDeliveryMethodMutation(active: false);

        warehouse.AddDeliveryMethod(newDeliveryMethodMutation);

        var result = warehouse.IsAvailable();

        Assert.That(result, Is.False);
    }
}