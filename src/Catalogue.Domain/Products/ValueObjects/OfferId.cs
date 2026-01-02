using Shared.Domain.Aggregates.ValueObjects;
using Shared.Domain.Guard;

namespace Catalogue.Domain.Products.ValueObjects;

public readonly record struct OfferId : IIntegerValueObject<OfferId>
{
    public int Value { get; }

    private OfferId(int value)
    {
        Value = value;
    }

    public static OfferId Create(int offerId)
    {
        DomainGuard.AgainstZero(offerId);

        return new OfferId(offerId);
    }


    public static implicit operator int(OfferId offerId)
    {
        return offerId.Value;
    }
}