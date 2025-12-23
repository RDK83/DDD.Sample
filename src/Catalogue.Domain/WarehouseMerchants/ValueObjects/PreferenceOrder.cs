using Shared.Domain.Aggregates.ValueObjects;
using Shared.Domain.Guard;

namespace Catalogue.Domain.WarehouseMerchants.ValueObjects;

public record PreferenceOrder : IIntegerValueObject<PreferenceOrder>, IComparable<PreferenceOrder>
{
    public int Value { get; }

    protected PreferenceOrder(int value)
    {
        Value = value;
    }

    public static PreferenceOrder Create(int preferenceOrder)
    {
        Guard.Against.Negative(preferenceOrder);
        return new PreferenceOrder(preferenceOrder);
    }

    public int CompareTo(PreferenceOrder? other)
    {
        if (other == null) return 1; // Consider 'this' as greater than null

        return Value.CompareTo(other.Value); // Compare based on the Value property
    }

    public static bool operator <(PreferenceOrder p1, PreferenceOrder p2)
    {
        return p1.Value < p2.Value;
    }

    public static bool operator >(PreferenceOrder p1, PreferenceOrder p2)
    {
        return p1.Value > p2.Value;
    }

    public static implicit operator int(PreferenceOrder preferenceOrder)
    {
        return preferenceOrder.Value;
    }
}