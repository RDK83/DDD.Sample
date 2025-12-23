using Catalogue.Domain.Common.Enums;
using Catalogue.Domain.Lookups.ManufacturerClassifications.ValueObjects;
using Catalogue.Domain.Products.ValueObjects;
using Catalogue.Domain.ProductTypes.ValueObjects;

namespace Catalogue.Domain.Products.Mutations;

public record EditProductMutation(
    TaxClass TaxClass,
    ProductTypeId ProductTypeId,
    ProductSubTypeId ProductSubTypeId,
    ProductTitle ProductTitle,
    ProductDescription Description,
    ManufacturerClassificationId ManufacturerClassificationId
);