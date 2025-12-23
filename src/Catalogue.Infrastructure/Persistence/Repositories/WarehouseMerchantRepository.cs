using Catalogue.Domain.WarehouseMerchants;
using Catalogue.Domain.WarehouseMerchants.ValueObjects;
using Catalogue.Infrastructure.Persistence.Repositories.Abstract;

namespace Catalogue.Infrastructure.Persistence.Repositories;

public class WarehouseMerchantRepository : Repository<WarehouseMerchant, WarehouseMerchantId>, IWarehouseMerchantRepository
{
    public WarehouseMerchantRepository(ApplicationDbContext context) : base(context)
    {
    }

    public virtual void Remove(WarehouseMerchantId id)
    {
        var dbSet = ApplicationDbContext.Set<WarehouseMerchant>();
        var entity = dbSet.Find(id);
        if (entity != null)
            dbSet.Remove(entity);
    }
}