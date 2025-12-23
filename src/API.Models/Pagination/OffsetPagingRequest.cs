using System.ComponentModel.DataAnnotations;

namespace API.Models.Pagination;

public record OffsetPagingRequest
{
    [Range(1, 100000)]
    public int PageNumber { get; init; } = 1;

    [Range(1, 100)]
    public int PageSize { get; init; } = 10;
}