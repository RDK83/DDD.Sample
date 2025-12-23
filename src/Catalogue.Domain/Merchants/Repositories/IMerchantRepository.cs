using SharedKernel.Repositories;

namespace Catalogue.Domain.Merchants.Repositories;

public interface IMerchantRepository : IRepository<Merchant>, IMerchantReadRepository
{
}