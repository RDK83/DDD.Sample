namespace API.Models.Pagination;

public record KeysetPagedResponse<T, TKey>(
    IReadOnlyList<T> Items,
    TKey? LastKey
);