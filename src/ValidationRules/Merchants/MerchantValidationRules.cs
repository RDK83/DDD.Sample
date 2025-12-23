namespace ValidationRules.Merchants;

public static class MerchantValidationRules
{
    public const int MerchantNameMaxLength = 150;
    public const int MerchantCodeMaxLength = 10;
    public const int AddressLineMaxLength = AddressValidationRules.AddressLineMaxLength;
    public const int CityMaxLength = AddressValidationRules.CityMaxLength;
    public const int PostCodeMaxLength = AddressValidationRules.PostCodeMaxLength;
    public const int CountryIsoCodeLength = AddressValidationRules.CountryCodeLength;
    public const int CountyMaxLength = AddressValidationRules.CountyMaxLength;
}