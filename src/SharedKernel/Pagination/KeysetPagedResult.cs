namespace SharedKernel.Pagination;

public record KeysetPagedResult<T, TKey>(
    IReadOnlyList<T> Items,
    TKey? LastKey
);