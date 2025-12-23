using Catalogue.Application.Warehouses.Commands;
using Catalogue.Application.Warehouses.Responses;

namespace Catalogue.Application.Warehouses;

public interface IWarehouseService
{
    Task<WarehouseDto> GetByIdAsync(int warehouseId);

    Task DeleteAsync(DeleteWarehouseCommand command);

    Task<WarehouseDto> CreateAsync(CreateWarehouseCommand command);

    Task UpdateAsync(UpdateWarehouseCommand command);
}