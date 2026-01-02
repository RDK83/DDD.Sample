using Shared.Domain.Aggregates.ValueObjects;
using Shared.Domain.Guard;

namespace Catalogue.Domain.Lookups.ManufacturerClassifications.ValueObjects;

public readonly record struct ManufacturerClassificationId : IIntegerValueObject<ManufacturerClassificationId>
{
    public int Value { get; }

    private ManufacturerClassificationId(int value)
    {
        Value = value;
    }

    public static ManufacturerClassificationId Create(int manufacturerClassificationId)
    {
        DomainGuard.AgainstZero(manufacturerClassificationId);

        return new ManufacturerClassificationId(manufacturerClassificationId);
    }

    public static implicit operator int(ManufacturerClassificationId manufacturerClassificationId)
    {
        return manufacturerClassificationId.Value;
    }
}