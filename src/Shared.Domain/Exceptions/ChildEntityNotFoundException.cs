using Shared.Domain.Exceptions.Abstract;

namespace Shared.Domain.Exceptions;

public class ChildEntityNotFoundException : ChildEntityException
{
    public ChildEntityNotFoundException(string parentEntityName, string childEntityName, string parentIdentifier, string childIdentifier) :
        base(parentEntityName, childEntityName, parentIdentifier, childIdentifier)
    {
    }

    public override string Message =>
        $"{ChildEntityName} with Id: '{ChildIdentifier}' was not found on {ParentEntityName} with Id '{ParentIdentifier}'.";
}