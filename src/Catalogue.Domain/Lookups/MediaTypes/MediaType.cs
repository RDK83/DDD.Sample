using Catalogue.Domain.Lookups.MediaTypes.ValueObjects;
using Shared.Domain.Guard;
using Shared.Domain.Lookups;
using ValidationRules.Lookups;

namespace Catalogue.Domain.Lookups.MediaTypes;

public record MediaType : LookupObject<MediaTypeId>
{
    public string Name { get; private set; }

    private MediaType(string name)
    {
        Name = name;
    }

    public static MediaType Create(string name)
    {
        DomainGuard.AgainstNullOrWhiteSpace(name);
        DomainGuard.AgainstStringTooLong(name, MediaTypeValidationRules.NameMaxLength);

        return new MediaType(name);
    }
}