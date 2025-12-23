using Catalogue.Domain.Medias.ValueObjects;
using Catalogue.Domain.Products.ValueObjects;
using Shared.Domain.Aggregates.Entities;

namespace Catalogue.Domain.Products.Entities;

public class ProductMedia : BaseEntity<ProductCode, MediaId>
{
    public ProductCode ProductCode { get; }
    public MediaId MediaId { get; }

    private ProductMedia(ProductCode productCode, MediaId mediaId)
    {
        ProductCode = productCode;
        MediaId = mediaId;
    }

    internal static ProductMedia Create(ProductCode productCode, MediaId mediaId)
    {
        return new ProductMedia(productCode, mediaId);
    }
}