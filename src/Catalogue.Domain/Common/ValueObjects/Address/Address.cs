using Shared.Domain.Aggregates.ValueObjects;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
namespace Catalogue.Domain.Common.ValueObjects.Address;

public class Address : IValueObject
{
    private Address()
    {
        //required by EF as this entity has Owned types in config
    }

    public PrimaryAddressLine AddressLine1 { get; }

    public AddressLine AddressLine2 { get; }

    public AddressLine AddressLine3 { get; }

    public AddressLine AddressLine4 { get; }

    public City City { get; }

    public County County { get; }

    public PostalCode PostalCode { get; }

    private Address(PrimaryAddressLine addressLine1, AddressLine addressLine2, AddressLine addressLine3,
        AddressLine addressLine4, City city,
        County county, PostalCode postalCode)
    {
        AddressLine1 = addressLine1;
        AddressLine2 = addressLine2;
        AddressLine3 = addressLine3;
        AddressLine4 = addressLine4;
        City = city;
        County = county;
        PostalCode = postalCode;
    }

    public static Address Create(PrimaryAddressLine addressLine1, AddressLine addressLine2, AddressLine addressLine3,
        AddressLine addressLine4,
        City city, County county, PostalCode postalCode)
    {
        return new Address(addressLine1, addressLine2, addressLine3, addressLine4, city, county, postalCode);
    }
}