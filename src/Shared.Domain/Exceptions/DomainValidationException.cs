using Shared.Domain.Exceptions.Abstract;

namespace Shared.Domain.Exceptions;

public class DomainValidationException(string message, string? parameterName = null) : DomainException(message)
{
    public string? ParameterName { get; } = parameterName;
}