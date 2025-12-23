using Catalogue.Domain.Common.Enums;
using Shared.Domain.Aggregates.ValueObjects;
using Shared.Domain.Guard;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

namespace Catalogue.Domain.Common.ValueObjects;

public record Money : IValueObject<Money, Currency, decimal>
{
    private Money()
    {
        //EF
    }

    public Currency Currency { get; }
    public decimal Value { get; }

    private Money(Currency currency, decimal value)
    {
        Value = value;
        Currency = currency;
    }


    public static Money Create(Currency currencyIsoCode, decimal value)
    {
        Guard.Against.Negative(value);

        return new Money(currencyIsoCode, value);
    }

    public static Money Create(decimal value)
    {
        Guard.Against.Negative(value);

        return new Money(Currency.GBP, value);
    }

    public static implicit operator decimal(Money money)
    {
        return money.Value;
    }
}