using Catalogue.Domain.UnitTests.Common;
using Catalogue.Domain.Warehouses.ValueObjects;
using Shared.Domain.Exceptions;

namespace Catalogue.Domain.UnitTests.Warehouses;

internal partial class WarehouseTests
{
    [Test]
    public void UpdateDetails_WithValidInput_ShouldUpdateSuccessfully()
    {
        var warehouse = WarehouseTestHelper.CreateWarehouse();

        var mutation = WarehouseTestHelper.CreateEditWarehouseMutation();

        warehouse.UpdateDetails(mutation);

        Assert.Multiple(() =>
        {
            Assert.That(warehouse, Is.Not.Null);
            Assert.That(warehouse.WarehouseName, Is.EqualTo(mutation.WarehouseName));
            Assert.That(warehouse.Active, Is.EqualTo(mutation.Active));
        });
    }

    [Test]
    public void UpdateDetails_WithNullInput_ShouldThrow()
    {
        var warehouse = WarehouseTestHelper.CreateWarehouse();

        Assert.Throws<DomainValidationException>(() => warehouse.UpdateDetails(null!));
    }

    [Test]
    public void AddDeliveryMethod_WithValidInput_ShouldAddSuccessfully()
    {
        var warehouse = WarehouseTestHelper.CreateWarehouse();

        var mutation = WarehouseTestHelper.CreateNewWarehouseDeliveryMethodMutation();

        warehouse.AddDeliveryMethod(mutation);

        Assert.That(warehouse.WarehouseDeliveryMethods.Count, Is.EqualTo(1));
    }

    [Test]
    public void AddDeliveryMethod_WithNullInput_ShouldThrow()
    {
        var warehouse = WarehouseTestHelper.CreateWarehouse();

        Assert.Throws<DomainValidationException>(() => warehouse.AddDeliveryMethod(null!));
    }

    [Test]
    public void AddDeliveryMethod_WithDuplicateInput_ShouldThrow()
    {
        var warehouse = WarehouseTestHelper.CreateWarehouse();

        var mutation = WarehouseTestHelper.CreateNewWarehouseDeliveryMethodMutation();

        warehouse.AddDeliveryMethod(mutation);

        Assert.Throws<ChildEntityAlreadyExistsException>(() => warehouse.AddDeliveryMethod(mutation));
    }

    [Test]
    public void RemoveDeliveryMethod_WhenChildExists_ShouldRemoveSuccessfully()
    {
        var warehouse = WarehouseTestHelper.CreateWarehouse();

        var mutation = WarehouseTestHelper.CreateNewWarehouseDeliveryMethodMutation();

        warehouse.AddDeliveryMethod(mutation);

        var deliveryMethodToRemove = warehouse.WarehouseDeliveryMethods.First();
        var fakeWdmId = WarehouseDeliveryMethodId.Create(1);
        TestReflectionHelper.SetAutoPropertyBackingField(deliveryMethodToRemove, nameof(deliveryMethodToRemove.Id),
            fakeWdmId);

        warehouse.RemoveDeliveryMethod(fakeWdmId);

        Assert.That(warehouse.WarehouseDeliveryMethods.Count, Is.Zero);
    }

    [Test]
    public void RemoveDeliveryMethod_WhenChildDoesNotExist_ShouldThrow()
    {
        var warehouse = WarehouseTestHelper.CreateWarehouse();
        var fakeDelMethId = WarehouseDeliveryMethodId.Create(1);
        Assert.Throws<ChildEntityNotFoundException>(() => warehouse.RemoveDeliveryMethod(fakeDelMethId));
    }
}