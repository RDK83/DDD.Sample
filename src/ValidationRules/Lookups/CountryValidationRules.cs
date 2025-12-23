namespace ValidationRules.Lookups;

public static class CountryValidationRules
{
    public const int CountryCodeLength = 2;
    public const int CountryNameMaxLength = 150;
    public const int DefaultCurrencyLength = CurrencyValidationRules.CurrencyCodeLength;
}