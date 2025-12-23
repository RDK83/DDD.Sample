namespace Shared.Domain.Aggregates.Entities;

public class SharedEntity<TId> : BaseEntity<TId>
{
    //Strictly not something that should be done in DDD but due to our schema it's a necessity.
    //This model purely serves to decorate a class in a way that reveals that it is shared by multiple AR
    //Which BaseEntity does not imply
}