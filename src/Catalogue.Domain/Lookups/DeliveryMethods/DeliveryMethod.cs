using Catalogue.Domain.Lookups.DeliveryMethods.ValueObjects;
using Shared.Domain.Guard;
using Shared.Domain.Lookups;
using ValidationRules.Lookups;

namespace Catalogue.Domain.Lookups.DeliveryMethods;

public record DeliveryMethod : LookupObject<DeliveryMethodId>
{
    public string Name { get; private set; }
    public bool Active { get; private set; }

    protected DeliveryMethod(DeliveryMethodId id, string name, bool active)
    {
        Id = id;
        Name = name;
        Active = active;
    }

    public static DeliveryMethod Create(DeliveryMethodId id, string name, bool active)
    {
        DomainGuard.AgainstNullOrWhiteSpace(name);

        name = name.Trim();

        DomainGuard.AgainstStringTooLong(name, DeliveryMethodValidationRules.DeliveryMethodNameMaxLength);

        return new DeliveryMethod(id, name, active);
    }
}