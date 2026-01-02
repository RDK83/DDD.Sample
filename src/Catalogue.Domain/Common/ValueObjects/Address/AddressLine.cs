using Shared.Domain.Aggregates.ValueObjects;
using Shared.Domain.Guard;
using ValidationRules;

namespace Catalogue.Domain.Common.ValueObjects.Address;

public record AddressLine : INullableStringValueObject<AddressLine>
{
    public string? Value { get; }
    public static int MaxLength => AddressValidationRules.AddressLineMaxLength;

    private AddressLine(string? value)
    {
        Value = value;
    }

    public static AddressLine Create(string? addressLine)
    {
        if (addressLine is null)
            return new AddressLine(addressLine);

        addressLine = addressLine.Trim();

        DomainGuard.AgainstStringTooLong(addressLine, MaxLength);

        return new AddressLine(addressLine);
    }

    public static implicit operator string?(AddressLine addressLine)
    {
        return addressLine.Value;
    }
}