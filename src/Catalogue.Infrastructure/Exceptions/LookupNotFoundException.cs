using Catalogue.Infrastructure.Exceptions.Abstract;

namespace Catalogue.Infrastructure.Exceptions;

[Serializable]
public sealed class LookupNotFoundException : PersistenceException
{
    public string LookupName { get; }
    public string? Identifier { get; }

    public LookupNotFoundException(string lookupName, string? identifier)
        : base($"{lookupName} with identifier '{identifier}' was not found.")
    {
        LookupName = lookupName;
        Identifier = identifier;
    }
}