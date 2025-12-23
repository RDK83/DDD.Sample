using Catalogue.Domain.Lookups.ManufacturerClassifications.ValueObjects;
using Shared.Domain.Guard;
using Shared.Domain.Lookups;

namespace Catalogue.Domain.Lookups.ManufacturerClassifications;

public record ManufacturerClassification : LookupObject<ManufacturerClassificationId>
{
    public string Description { get; }

    public bool Active { get; }

    private ManufacturerClassification(string description, bool active)
    {
        Description = description;
        Active = active;
    }

    public static ManufacturerClassification Create(string description, bool active)
    {
        Guard.Against.NullOrWhiteSpace(description);

        description = description.Trim();

        return new ManufacturerClassification(description, active);
    }
}