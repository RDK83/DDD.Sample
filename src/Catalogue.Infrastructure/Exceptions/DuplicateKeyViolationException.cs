using Catalogue.Infrastructure.Exceptions.Abstract;

namespace Catalogue.Infrastructure.Exceptions;

public class DuplicateKeyViolationException(string message) : PersistenceException(message)
{
}