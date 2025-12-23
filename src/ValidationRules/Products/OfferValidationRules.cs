using ValidationRules.Lookups;

namespace ValidationRules.Products;

public class OfferValidationRules
{
    public const int CurrencyIsoCodeLength = CurrencyValidationRules.CurrencyCodeLength;
    public const int ProductCodeMaxLength = ProductValidationRules.ProductCodeMaxLength;
    public const int BomInfoMaxLength = 4000;
}