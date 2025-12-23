using System.ComponentModel.DataAnnotations;

namespace API.Models.RouteValues.Media;

public record MediaRouteValues : IRouteValues
{
    [Range(1, int.MaxValue)]
    public required int MediaId { get; init; }
}