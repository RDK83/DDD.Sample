using Shared.Domain.Aggregates.ValueObjects;
using Shared.Domain.Guard;
using ValidationRules;

namespace Catalogue.Domain.Common.ValueObjects.Address;

public record City : IStringValueObject<City>
{
    public string Value { get; }
    public static int MaxLength => AddressValidationRules.CityMaxLength;

    private City(string value)
    {
        Value = value;
    }

    public static City Create(string city)
    {
        Guard.Against.NullOrWhiteSpace(city);

        city = city.Trim();

        Guard.Against.StringTooLong(city, MaxLength);

        return new City(city);
    }

    public static implicit operator string(City city)
    {
        return city.Value;
    }
}