using Catalogue.Infrastructure.Exceptions.Abstract;

namespace Catalogue.Infrastructure.Exceptions;

public class InvalidSpecificationException(string message) : PersistenceException(message);