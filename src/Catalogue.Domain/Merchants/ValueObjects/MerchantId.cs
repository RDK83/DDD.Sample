using Shared.Domain.Aggregates.ValueObjects;
using Shared.Domain.Guard;

namespace Catalogue.Domain.Merchants.ValueObjects;

public readonly record struct MerchantId : IIntegerValueObject<MerchantId>
{
    public int Value { get; }

    private MerchantId(int value)
    {
        Value = value;
    }

    public static MerchantId Create(int merchantId)
    {
        Guard.Against.Zero(merchantId);

        return new MerchantId(merchantId);
    }

    public static implicit operator int(MerchantId merchantId)
    {
        return merchantId.Value;
    }
}