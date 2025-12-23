using Catalogue.Application.WarehouseMerchants.Commands;
using Catalogue.Application.WarehouseMerchants.Queries;
using Catalogue.Application.WarehouseMerchants.Responses;

namespace Catalogue.Application.WarehouseMerchants;

public interface IWarehouseMerchantService
{
    Task<List<WarehouseMerchantDto>> GetAllAsync();
    Task<WarehouseMerchantDto> GetByIdAsync(GetWarehouseMerchantByIdQuery query);
    Task<WarehouseMerchantDto> CreateAsync(CreateWarehouseMerchantCommand command);
    Task DeleteAsync(DeleteWarehouseMerchantCommand command);
    Task<WarehouseMerchantDto> UpdateAsync(UpdateWarehouseMerchantCommand command);
}