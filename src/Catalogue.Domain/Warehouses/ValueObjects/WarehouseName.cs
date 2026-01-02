using Shared.Domain.Aggregates.ValueObjects;
using Shared.Domain.Guard;
using ValidationRules.Warehouses;

namespace Catalogue.Domain.Warehouses.ValueObjects;

public record WarehouseName : IStringValueObject<WarehouseName>
{
    public string Value { get; }
    public static int MaxLength => WarehouseValidationRules.WarehouseNameMaxLength;

    protected WarehouseName(string value)
    {
        Value = value;
    }

    public static WarehouseName Create(string warehouseName)
    {
        DomainGuard.AgainstNullOrWhiteSpace(warehouseName);

        warehouseName = warehouseName.Trim();

        DomainGuard.AgainstStringTooLong(warehouseName, MaxLength);

        return new WarehouseName(warehouseName);
    }

    public static implicit operator string(WarehouseName warehouseName)
    {
        return warehouseName.Value;
    }
}