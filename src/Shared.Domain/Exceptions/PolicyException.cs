using Shared.Domain.Exceptions.Abstract;

namespace Shared.Domain.Exceptions;

public class PolicyException : DomainException
{
    public string PolicyName { get; }

    public PolicyException(string policyName, string message) : base(message)
    {
        PolicyName = policyName;
    }
}