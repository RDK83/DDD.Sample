using Catalogue.Domain.Warehouses;
using Catalogue.Domain.Warehouses.Repositories;
using Catalogue.Domain.Warehouses.ValueObjects;
using Catalogue.Infrastructure.Persistence.Repositories.Abstract;

namespace Catalogue.Infrastructure.Persistence.Repositories;

public sealed class WarehouseRepository : Repository<Warehouse, WarehouseId>, IWarehouseRepository
{
    public WarehouseRepository(ApplicationDbContext context) : base(context)
    {
    }
}