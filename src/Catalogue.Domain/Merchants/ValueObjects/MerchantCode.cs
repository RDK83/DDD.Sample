using Shared.Domain.Aggregates.ValueObjects;
using Shared.Domain.Guard;
using ValidationRules.Merchants;

namespace Catalogue.Domain.Merchants.ValueObjects;

public record MerchantCode : IStringValueObject<MerchantCode>
{
    public string Value { get; }
    public static int MaxLength => MerchantValidationRules.MerchantCodeMaxLength;

    protected MerchantCode(string value)
    {
        Value = value;
    }

    public static MerchantCode Create(string merchantCode)
    {
        Guard.Against.NullOrWhiteSpace(merchantCode);

        merchantCode = merchantCode.Trim().ToUpperInvariant();

        Guard.Against.StringTooLong(merchantCode, MaxLength);

        return new MerchantCode(merchantCode);
    }

    public static implicit operator string(MerchantCode merchantCode)
    {
        return merchantCode.Value;
    }
}