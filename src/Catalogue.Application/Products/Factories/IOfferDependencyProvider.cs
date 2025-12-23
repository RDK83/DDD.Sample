using Catalogue.Application.Products.Contexts;

namespace Catalogue.Application.Products.Factories;

public interface IOfferDependencyProvider
{
    Task<OfferDependencies> LoadAsync();
}