using Shared.Domain.Aggregates.ValueObjects;

namespace Catalogue.Domain.Products.ValueObjects;

public readonly record struct PromotedOfferId : INullableIntegerValueObject<PromotedOfferId>
{
    public int? Value { get; }

    private PromotedOfferId(int? value)
    {
        Value = value;
    }

    public static PromotedOfferId Create(int? promotedOfferId)
    {
        return new PromotedOfferId(promotedOfferId);
    }


    public static implicit operator int?(PromotedOfferId promotedOfferId)
    {
        return promotedOfferId.Value;
    }
}