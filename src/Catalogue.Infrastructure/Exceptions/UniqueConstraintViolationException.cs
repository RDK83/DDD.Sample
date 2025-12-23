using Catalogue.Infrastructure.Exceptions.Abstract;

namespace Catalogue.Infrastructure.Exceptions;

public class UniqueConstraintViolationException(string message) : PersistenceException(message)
{
}