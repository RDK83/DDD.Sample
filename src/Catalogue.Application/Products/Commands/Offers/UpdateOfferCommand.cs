using Catalogue.Domain.Common.ValueObjects;
using Catalogue.Domain.Products.Mutations;

namespace Catalogue.Application.Products.Commands.Offers;

public record UpdateOfferCommand(
    int Id,
    string ProductCode,
    decimal GrossPrice,
    int StockLevel
)
{
    public EditOfferMutation ToMutation()
    {
        return new EditOfferMutation(
            GrossPrice: Money.Create(GrossPrice),
            StockLevel: Domain.Products.ValueObjects.StockLevel.Create(StockLevel)
        );
    }
}