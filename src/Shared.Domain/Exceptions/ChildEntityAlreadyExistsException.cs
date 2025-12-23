using Shared.Domain.Exceptions.Abstract;

namespace Shared.Domain.Exceptions;

public class ChildEntityAlreadyExistsException : ChildEntityException
{
    public ChildEntityAlreadyExistsException(string parentEntityName, string childEntityName, string parentIdentifier, string childIdentifier) :
        base(parentEntityName, childEntityName, parentIdentifier, childIdentifier)
    {
    }

    public override string Message =>
        $"{ChildEntityName} with Id '{ChildIdentifier}' already exists on {ParentEntityName} with Id '{ParentIdentifier}'.";
}