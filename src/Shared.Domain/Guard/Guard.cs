namespace Shared.Domain.Guard;

public class Guard : IGuardClause
{
    public static IGuardClause Against { get; } = new Guard();

    private Guard()
    {
    }
}