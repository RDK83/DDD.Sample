using Catalogue.Application.Merchants.Commands;
using Catalogue.Application.Merchants.Queries;
using Catalogue.Application.Merchants.Responses;

namespace Catalogue.Application.Merchants;

public interface IMerchantService
{
    Task<MerchantDto> GetByIdAsync(GetMerchantByIdQuery query);
    Task<MerchantDto> CreateAsync(CreateMerchantCommand product);
    Task UpdateAsync(UpdateMerchantCommand command);
    Task DeleteAsync(DeleteMerchantCommand command);
}