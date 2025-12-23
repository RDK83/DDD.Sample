namespace API.Models.Response.Products;

public record ProductResponse(
    string ProductCode,
    string ManufacturerClassification,
    string ProductTitle,
    string Description,
    string TaxClass,
    int ManufacturerClassificationId,
    int ProductSubTypeId,
    int ProductTypeId,
    int? PromotedOfferId,
    DateTime LastUpdated,
    IReadOnlyCollection<OfferResponse> Offers,
    IReadOnlyCollection<ProductMediaResponse> Medias);