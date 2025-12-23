using Shared.Domain.Aggregates.ValueObjects;
using Shared.Domain.Guard;
using ValidationRules.Merchants;

namespace Catalogue.Domain.Merchants.ValueObjects;

public record MerchantName : IStringValueObject<MerchantName>
{
    public string Value { get; }
    public static int MaxLength => MerchantValidationRules.MerchantNameMaxLength;

    private MerchantName(string value)
    {
        Value = value;
    }

    public static MerchantName Create(string merchantName)
    {
        Guard.Against.NullOrWhiteSpace(merchantName);

        merchantName = merchantName.Trim();

        Guard.Against.StringTooLong(merchantName, MaxLength);

        return new MerchantName(merchantName);
    }

    public static implicit operator string(MerchantName merchantName)
    {
        return merchantName.Value;
    }
}