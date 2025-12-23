using Shared.Domain.Aggregates.ValueObjects;
using Shared.Domain.Guard;
using ValidationRules.Lookups;

namespace Catalogue.Domain.Lookups.DeliveryMethods.ValueObjects;

public record DeliveryMethodId : IStringValueObject<DeliveryMethodId>
{
    public string Value { get; }
    public static int MinLength => DeliveryMethodValidationRules.DeliveryMethodIdLength;
    public static int MaxLength => DeliveryMethodValidationRules.DeliveryMethodIdLength;


    private DeliveryMethodId(string value)
    {
        Value = value.ToUpperInvariant();
    }

    public static DeliveryMethodId Create(string deliveryMethodId)
    {
        Guard.Against.NullOrWhiteSpace(deliveryMethodId);

        deliveryMethodId = deliveryMethodId.Trim();

        Guard.Against.LengthOutOfRange(deliveryMethodId, MinLength, MaxLength);

        return new DeliveryMethodId(deliveryMethodId);
    }

    public static implicit operator string(DeliveryMethodId deliveryMethodId)
    {
        return deliveryMethodId.Value;
    }
}