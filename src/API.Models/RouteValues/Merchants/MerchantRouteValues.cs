using System.ComponentModel.DataAnnotations;

namespace API.Models.RouteValues.Merchants;

public record MerchantRouteValues : IRouteValues
{
    [Range(1, int.MaxValue)]
    public required int MerchantId { get; init; }
}