using System.ComponentModel.DataAnnotations;

namespace API.Models.Pagination.Abstract;

public abstract record BaseKeysetPagingRequest
{
    public bool Descending { get; init; } = false;

    [Range(1, 1000)]
    public int PageSize { get; init; } = 20;
}