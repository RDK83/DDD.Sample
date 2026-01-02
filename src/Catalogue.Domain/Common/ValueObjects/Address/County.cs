using Shared.Domain.Aggregates.ValueObjects;
using Shared.Domain.Guard;
using ValidationRules;

namespace Catalogue.Domain.Common.ValueObjects.Address;

public record County : INullableStringValueObject<County>
{
    public string? Value { get; }
    public static int MaxLength => AddressValidationRules.CountyMaxLength;

    private County(string? value)
    {
        Value = value;
    }

    public static County Create(string? county)
    {
        if (county is null)
            return new County(county);

        county = county.Trim();

        DomainGuard.AgainstStringTooLong(county, MaxLength);

        return new County(county);
    }

    public static implicit operator string?(County county)
    {
        return county.Value;
    }
}