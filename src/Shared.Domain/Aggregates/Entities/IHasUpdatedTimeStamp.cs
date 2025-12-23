namespace Shared.Domain.Aggregates.Entities;

public interface IHasUpdatedTimeStamp
{
    DateTime UpdatedAt { get; set; }
}