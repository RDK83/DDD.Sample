namespace Shared.Application.Pagination;

public record PagingQuery(
    int PageNumber = 1,
    int PageSize = 20,
    bool Descending = false,
    string? Cursor = null
);