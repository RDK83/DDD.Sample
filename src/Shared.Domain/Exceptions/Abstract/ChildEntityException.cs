namespace Shared.Domain.Exceptions.Abstract;

public abstract class ChildEntityException : DomainException
{
    public string ParentEntityName { get; init; }
    public string ChildEntityName { get; init; }
    public string ParentIdentifier { get; init; }
    public string ChildIdentifier { get; init; }

    protected ChildEntityException(
        string parentEntityName,
        string childEntityName,
        string parentIdentifier,
        string childIdentifier
    )
    {
        ParentEntityName = parentEntityName;
        ChildEntityName = childEntityName;
        ParentIdentifier = parentIdentifier;
        ChildIdentifier = childIdentifier;
    }
}