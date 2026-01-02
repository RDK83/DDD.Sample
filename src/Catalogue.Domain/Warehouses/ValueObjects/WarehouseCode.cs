using Shared.Domain.Aggregates.ValueObjects;
using Shared.Domain.Guard;
using ValidationRules.Warehouses;

namespace Catalogue.Domain.Warehouses.ValueObjects;

public record WarehouseCode : IStringValueObject<WarehouseCode>
{
    public string Value { get; }
    public static int MaxLength => WarehouseValidationRules.WarehouseCodeMaxLength;

    protected WarehouseCode(string value)
    {
        Value = value;
    }

    public static WarehouseCode Create(string warehouseCode)
    {
        DomainGuard.AgainstNullOrWhiteSpace(warehouseCode);

        warehouseCode = warehouseCode.Trim().ToUpperInvariant();

        DomainGuard.AgainstStringTooLong(warehouseCode, MaxLength);

        return new WarehouseCode(warehouseCode);
    }

    public static implicit operator string(WarehouseCode warehouseCode)
    {
        return warehouseCode.Value;
    }
}