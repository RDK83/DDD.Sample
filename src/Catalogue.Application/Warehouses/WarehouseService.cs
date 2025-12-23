using Catalogue.Application.Shared.Logging;
using Catalogue.Application.Warehouses.Commands;
using Catalogue.Application.Warehouses.Responses;
using Catalogue.Application.Warehouses.Specifications.Helper;
using Catalogue.Domain.Warehouses.Repositories;
using Catalogue.Domain.Warehouses.ValueObjects;
using Shared.Application.UnitOfWork;

namespace Catalogue.Application.Warehouses;

[LogMethod]
[LogParameters]
public class WarehouseService(
    IWarehouseRepository repository,
    IUnitOfWork unitOfWork)
    : IWarehouseService
{
    public async Task<WarehouseDto> GetByIdAsync(int warehouseId)
    {
        var id = WarehouseId.Create(warehouseId);

        var warehouse = await repository.GetFirstBySpecificationAsync(WarehouseSpecs.ById(id));

        var dto = WarehouseDto.FromEntity(warehouse);

        return dto;
    }

    public async Task<WarehouseDto> CreateAsync(CreateWarehouseCommand command)
    {
        var newWarehouse = command.ToEntity();

        repository.Add(newWarehouse);

        await unitOfWork.SaveChangesAsync();

        return WarehouseDto.FromEntity(newWarehouse);
    }

    public async Task UpdateAsync(UpdateWarehouseCommand command)
    {
        var id = WarehouseId.Create(command.WarehouseId);
        var warehouse = await repository.GetFirstBySpecificationAsync(WarehouseSpecs.ById(id));

        var mutation = command.ToMutation();
        warehouse.UpdateDetails(mutation);

        await unitOfWork.SaveChangesAsync();
    }

    public async Task DeleteAsync(DeleteWarehouseCommand command)
    {
        var id = WarehouseId.Create(command.WarehouseId);
        var warehouse = await repository.GetFirstBySpecificationAsync(WarehouseSpecs.ById(id));

        repository.Remove(warehouse);

        await unitOfWork.SaveChangesAsync();
    }
}