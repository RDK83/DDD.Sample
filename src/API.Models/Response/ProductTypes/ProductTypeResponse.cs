namespace API.Models.Response.ProductTypes;

public record ProductTypeResponse(
    string ProductTypeName,
    bool Active,
    IReadOnlyCollection<ProductSubTypeResponse> ProductSubTypes,
    IReadOnlyCollection<ProductTypeAttributeMappingResponse> ProductTypeAttributeMappings);