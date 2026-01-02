using Catalogue.Domain.Common.ValueObjects.Address;
using Catalogue.Domain.Warehouses.Entities;
using Catalogue.Domain.Warehouses.Mutations;
using Catalogue.Domain.Warehouses.ValueObjects;
using Shared.Domain.Aggregates;
using Shared.Domain.Exceptions;
using Shared.Domain.Guard;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

namespace Catalogue.Domain.Warehouses;

public class Warehouse : BaseAggregate<WarehouseId>
{
    public WarehouseName WarehouseName { get; private set; }

    public WarehouseCode WarehouseCode { get; private set; }

    public bool Active { get; private set; }

    public Address Address { get; private set; }

    public IReadOnlyCollection<WarehouseDeliveryMethod> WarehouseDeliveryMethods => _warehouseDeliveryMethods;

    private readonly List<WarehouseDeliveryMethod> _warehouseDeliveryMethods;


    protected Warehouse(WarehouseName name, WarehouseCode code, Address address, bool active)
    {
        WarehouseName = name;
        WarehouseCode = code;
        Address = address;
        Active = active;
        _warehouseDeliveryMethods = [];
    }

    private Warehouse()
    {
        //EF
    }

    public static Warehouse Create(WarehouseName warehouseName, WarehouseCode warehouseCode, Address address,
        bool active)
    {
        return new Warehouse(warehouseName, warehouseCode, address, active);
    }


    public void UpdateDetails(EditWarehouseMutation mutation)
    {
        DomainGuard.AgainstNull(mutation);

        WarehouseName = mutation.WarehouseName;
        Active = mutation.Active;

        Address = mutation.Address;
    }

    public bool IsAvailable()
    {
        if (!Active)
            return false;

        if (!HasActiveDeliveryMethod())
            return false;

        return true;
    }

    public void AddDeliveryMethod(NewWarehouseDeliveryMethodMutation mutation)
    {
        DomainGuard.AgainstNull(mutation);

        if (_warehouseDeliveryMethods.Any(wdm => wdm.DeliveryMethodId.Equals(mutation.DeliveryMethodId)))
            throw new ChildEntityAlreadyExistsException(nameof(Warehouse), nameof(WarehouseDeliveryMethod),
                Id.ToString(), mutation.DeliveryMethodId);

        var newDeliveryMethod = WarehouseDeliveryMethod.Create(
            warehouseId: Id,
            deliveryMethodId: mutation.DeliveryMethodId,
            countryIsoCode: mutation.DeliveryCountryIsoCode,
            grossCost: mutation.GrossCost,
            leadTimes: mutation.LeadTimes,
            active: mutation.Active
        );

        _warehouseDeliveryMethods.Add(newDeliveryMethod);
    }


    public void RemoveDeliveryMethod(WarehouseDeliveryMethodId deliveryMethodId)
    {
        var deliveryMethod = _warehouseDeliveryMethods.FirstOrDefault(x => x.Id.Equals(deliveryMethodId));

        if (deliveryMethod is null)
            throw new ChildEntityNotFoundException(nameof(Warehouse), nameof(WarehouseDeliveryMethod),
                Id.Value.ToString(), deliveryMethodId.Value.ToString());

        _warehouseDeliveryMethods.Remove(deliveryMethod);
    }

    private bool HasActiveDeliveryMethod()
    {
        if (_warehouseDeliveryMethods.Any(wdm => wdm.Active))
            return true;

        return false;
    }
}