namespace ValidationRules.Warehouses;

public static class WarehouseValidationRules
{
    public const int WarehouseNameMaxLength = 150;
    public const int WarehouseCodeMaxLength = 10;
    public const int AddressLineMaxLength = AddressValidationRules.AddressLineMaxLength;
    public const int CityMaxLength = AddressValidationRules.CityMaxLength;
    public const int PostCodeMaxLength = AddressValidationRules.PostCodeMaxLength;
    public const int CountryIsCodeLength = AddressValidationRules.CountryCodeLength;
    public const int CountyMaxLength = AddressValidationRules.CountyMaxLength;
}