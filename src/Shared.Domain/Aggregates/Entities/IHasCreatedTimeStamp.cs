namespace Shared.Domain.Aggregates.Entities;

public interface IHasCreatedTimeStamp
{
    DateTime CreatedAt { get; set; }
}