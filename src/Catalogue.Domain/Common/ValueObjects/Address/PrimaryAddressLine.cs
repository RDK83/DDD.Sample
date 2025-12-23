using Shared.Domain.Aggregates.ValueObjects;
using Shared.Domain.Guard;
using ValidationRules;

namespace Catalogue.Domain.Common.ValueObjects.Address;

public record PrimaryAddressLine : IStringValueObject<PrimaryAddressLine>
{
    public string Value { get; }
    public static int MaxLength => AddressValidationRules.AddressLineMaxLength;

    private PrimaryAddressLine(string value)
    {
        Value = value;
    }

    public static PrimaryAddressLine Create(string addressLine)
    {
        Guard.Against.NullOrWhiteSpace(addressLine);

        addressLine = addressLine.Trim();

        Guard.Against.StringTooLong(addressLine, MaxLength);

        return new PrimaryAddressLine(addressLine);
    }

    public static implicit operator string(PrimaryAddressLine addressLine)
    {
        return addressLine.Value;
    }
}