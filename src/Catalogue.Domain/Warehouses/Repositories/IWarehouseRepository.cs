using SharedKernel.Repositories;

namespace Catalogue.Domain.Warehouses.Repositories;

public interface IWarehouseRepository : IRepository<Warehouse>, IWarehouseReadRepository
{
}