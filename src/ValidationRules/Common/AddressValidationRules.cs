using ValidationRules.Lookups;

namespace ValidationRules;

public static class AddressValidationRules
{
    public const int AddressLineMaxLength = 150;
    public const int CityMaxLength = 100;
    public const int CountyMaxLength = 100;
    public const int PostCodeMaxLength = 50;
    public const int CountryCodeLength = CountryValidationRules.CountryCodeLength;
}