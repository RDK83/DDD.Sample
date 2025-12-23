using ValidationRules.Lookups;

namespace ValidationRules.Warehouses;

public static class WarehouseDeliveryMethodValidationRules
{
    public const int CurrencyIsoCodeLength = CurrencyValidationRules.CurrencyCodeLength;
    public const int DeliveryMethodIdLength = DeliveryMethodValidationRules.DeliveryMethodIdLength;
    public const int NoteMaxLength = 150;
}