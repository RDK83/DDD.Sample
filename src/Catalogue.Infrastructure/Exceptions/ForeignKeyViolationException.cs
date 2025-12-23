using Catalogue.Infrastructure.Exceptions.Abstract;

namespace Catalogue.Infrastructure.Exceptions;

public class ForeignKeyViolationException(string message) : PersistenceException(message)
{
}