namespace Catalogue.Application.Products.Queries;

public record ProductFilter
{
    public string? PublishedVersion { get; init; }

    public int? ProductTypeId { get; init; }

    public bool? InStock { get; init; }
}