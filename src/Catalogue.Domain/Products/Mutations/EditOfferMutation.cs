using Catalogue.Domain.Common.ValueObjects;
using Catalogue.Domain.Products.ValueObjects;

namespace Catalogue.Domain.Products.Mutations;

public record EditOfferMutation(
    Money GrossPrice,
    StockLevel StockLevel);