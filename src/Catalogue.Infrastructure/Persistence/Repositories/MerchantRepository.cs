using Catalogue.Domain.Merchants;
using Catalogue.Domain.Merchants.Repositories;
using Catalogue.Domain.Merchants.ValueObjects;
using Catalogue.Infrastructure.Persistence.Repositories.Abstract;

namespace Catalogue.Infrastructure.Persistence.Repositories;

public class MerchantRepository : Repository<Merchant, MerchantId>, IMerchantRepository
{
    public MerchantRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
    {
    }
}