using SharedKernel.Repositories;

namespace Catalogue.Domain.ProductTypes.Repositories;

public interface IProductTypeRepository : IRepository<ProductType>, IProductTypeReadRepository
{
}