using Catalogue.Domain.Common.Enums;
using Catalogue.Domain.Common.ValueObjects;
using Catalogue.Domain.Common.ValueObjects.Address;
using Catalogue.Domain.Lookups.DeliveryMethods.ValueObjects;
using Catalogue.Domain.Warehouses;
using Catalogue.Domain.Warehouses.Mutations;
using Catalogue.Domain.Warehouses.ValueObjects;

namespace Catalogue.Domain.UnitTests.Warehouses;

internal class WarehouseTestHelper
{
    public static WarehouseId ValidWarehouseId() =>
        WarehouseId.Create(1);

    public static WarehouseCode ValidWarehouseCode() => WarehouseCode.Create("WINP");

    public static WarehouseDeliveryMethodId ValidWarehouseDeliveryMethodId() =>
        WarehouseDeliveryMethodId.Create(1);

    public static DeliveryMethodId ValidDeliveryMethodId() =>
        DeliveryMethodId.Create("NR");

    public static WarehouseDeliveryLeadTime ValidWarehouseDeliveryLeadTime() =>
        WarehouseDeliveryLeadTime.Create(1, 2);

    public static Country ValidCountryCode() => Country.GB;

    public static Address ValidAddress() =>
        Address.Create(
            PrimaryAddressLine.Create("Unit 1, Fairs Park"),
            addressLine2: AddressLine.Create("Turtle Way"),
            addressLine3: AddressLine.Create(null),
            addressLine4: AddressLine.Create(null),
            City.Create("Birmingham"),
            County.Create("West Midlands"),
            PostalCode.Create("B1 2PT", Country.GB)
        );

    public static Warehouse CreateWarehouse(bool active = true)
    {
        var entity = Warehouse.Create(
            WarehouseName.Create("Warehouse PreOrder"),
            ValidWarehouseCode(),
            ValidAddress(),
            active: active
        );

        return entity;
    }


    public static EditWarehouseMutation CreateEditWarehouseMutation()
    {
        var mutation = new EditWarehouseMutation(
            WarehouseName.Create("Warehouse NameChange"),
            ValidAddress(),
            true
        );

        return mutation;
    }

    public static NewWarehouseDeliveryMethodMutation CreateNewWarehouseDeliveryMethodMutation(bool active = true)
    {
        var mutation = new NewWarehouseDeliveryMethodMutation(
            ValidWarehouseId(),
            ValidDeliveryMethodId(),
            Country.GB,
            Money.Create(20m),
            ValidWarehouseDeliveryLeadTime(),
            active
        );

        return mutation;
    }
}