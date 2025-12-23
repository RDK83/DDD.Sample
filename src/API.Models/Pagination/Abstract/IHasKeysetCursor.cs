namespace API.Models.Pagination.Abstract;

public interface IHasKeysetCursor<T>
{
    public T? Cursor { get; init; }
}