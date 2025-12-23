using Catalogue.Domain.ProductTypes;
using Catalogue.Domain.ProductTypes.Repositories;
using Catalogue.Domain.ProductTypes.ValueObjects;
using Catalogue.Infrastructure.Persistence.Repositories.Abstract;

namespace Catalogue.Infrastructure.Persistence.Repositories;

public class ProductTypeRepository : Repository<ProductType, ProductTypeId>, IProductTypeRepository
{
    public ProductTypeRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
    {
    }
}