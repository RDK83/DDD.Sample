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
        Guard.Against.NullOrWhiteSpace(warehouseName);

        warehouseName = warehouseName.Trim();

        Guard.Against.StringTooLong(warehouseName, MaxLength);

        return new WarehouseName(warehouseName);
    }

    public static implicit operator string(WarehouseName warehouseName)
    {
        return warehouseName.Value;
    }
}