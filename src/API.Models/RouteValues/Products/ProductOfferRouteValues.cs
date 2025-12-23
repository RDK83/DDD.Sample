using System.ComponentModel.DataAnnotations;

namespace API.Models.RouteValues.Products;

public record ProductOfferRouteValues : ProductRouteValues
{
    [Range(1, int.MaxValue)]
    public required int OfferId { get; set; }
}