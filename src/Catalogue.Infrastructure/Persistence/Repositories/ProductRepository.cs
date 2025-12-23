using Catalogue.Domain.Products;
using Catalogue.Domain.Products.Repositories;
using Catalogue.Domain.Products.ValueObjects;
using Catalogue.Infrastructure.Persistence.Repositories.Abstract;

namespace Catalogue.Infrastructure.Persistence.Repositories;

public class ProductRepository : KeysetPagedRepository<Product, ProductCode, ProductCode>, IProductRepository
{
    public ProductRepository(ApplicationDbContext context) : base(context)
    {
    }
}