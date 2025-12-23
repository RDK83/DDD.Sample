namespace Catalogue.Infrastructure.Exceptions.Abstract;

public abstract class PersistenceException : Exception
{
    protected PersistenceException()
    {
    }

    protected PersistenceException(string message)
        : base(message)
    {
    }

    protected PersistenceException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}