using Catalogue.Infrastructure.Exceptions.Abstract;

namespace Catalogue.Infrastructure.Exceptions;

[Serializable]
public sealed class EntityNotFoundException : PersistenceException
{
    public string EntityName { get; }
    public string? Identifier { get; }

    public EntityNotFoundException(string entityName)
        : base($"{entityName} or {entityName}s were not found.")
    {
        EntityName = entityName;
    }
}