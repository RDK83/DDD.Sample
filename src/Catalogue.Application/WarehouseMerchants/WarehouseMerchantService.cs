using Catalogue.Application.Merchants.Specifications.Helper;
using Catalogue.Application.Shared.Logging;
using Catalogue.Application.WarehouseMerchants.Commands;
using Catalogue.Application.WarehouseMerchants.Queries;
using Catalogue.Application.WarehouseMerchants.Responses;
using Catalogue.Application.WarehouseMerchants.Specifications.Helper;
using Catalogue.Application.Warehouses.Specifications.Helper;
using Catalogue.Domain.Merchants.Repositories;
using Catalogue.Domain.Merchants.ValueObjects;
using Catalogue.Domain.WarehouseMerchants;
using Catalogue.Domain.WarehouseMerchants.Policies;
using Catalogue.Domain.WarehouseMerchants.ValueObjects;
using Catalogue.Domain.Warehouses.Repositories;
using Catalogue.Domain.Warehouses.ValueObjects;
using Shared.Application.UnitOfWork;

namespace Catalogue.Application.WarehouseMerchants;

[LogMethod]
[LogParameters]
public class WarehouseMerchantService(
    IWarehouseMerchantRepository warehouseMerchantRepository,
    IWarehouseReadRepository warehouseReadRepository,
    IMerchantReadRepository merchantReadRepository,
    IUnitOfWork unitOfWork) : IWarehouseMerchantService
{
    public async Task<List<WarehouseMerchantDto>> GetAllAsync()
    {
        var domainModels = await warehouseMerchantRepository.GetManyBySpecificationAsync(WarehouseMerchantSpecs.All());

        return domainModels.Select(WarehouseMerchantDto.FromEntity).ToList();
    }

    public async Task<WarehouseMerchantDto> GetByIdAsync(GetWarehouseMerchantByIdQuery query)
    {
        var warehouseMerchantId = WarehouseMerchantId.Create(query.WarehouseMerchantId);

        var domainModel =
            await warehouseMerchantRepository.GetFirstBySpecificationAsync(
                WarehouseMerchantSpecs.ById(warehouseMerchantId));

        return WarehouseMerchantDto.FromEntity(domainModel);
    }

    public async Task<WarehouseMerchantDto> CreateAsync(CreateWarehouseMerchantCommand command)
    {
        var warehouseId = WarehouseId.Create(command.WarehouseId);
        await warehouseReadRepository.VerifyExistsAsync(WarehouseSpecs.ById(warehouseId));

        var merchantId = MerchantId.Create(command.MerchantId);
        await merchantReadRepository.VerifyExistsAsync(MerchantSpecs.ById(merchantId));

        var existingWarehouseMerchants =
            await warehouseMerchantRepository.GetManyBySpecificationAsync(WarehouseMerchantSpecs.All());
        var existingPreferenceOrders = existingWarehouseMerchants.Select(x => x.PreferenceOrder).ToHashSet();

        var uniquenessPolicy = new WarehouseMerchantUniquePreferenceOrderPolicy(existingPreferenceOrders);
        var newWarehouseMerchant = command.ToEntity(uniquenessPolicy);

        warehouseMerchantRepository.Add(newWarehouseMerchant);
        await unitOfWork.SaveChangesAsync();

        return WarehouseMerchantDto.FromEntity(newWarehouseMerchant);
    }

    public async Task<WarehouseMerchantDto> UpdateAsync(UpdateWarehouseMerchantCommand updateWarehouseMerchantCommand)
    {
        var existingWarehouseMerchants =
            await warehouseMerchantRepository.GetManyBySpecificationAsync(WarehouseMerchantSpecs.All());
        var existingPreferenceOrders = existingWarehouseMerchants.Select(x => x.PreferenceOrder).ToHashSet();

        var warehouseMerchantId = WarehouseMerchantId.Create(updateWarehouseMerchantCommand.Id);
        var existingWarehouseMerchant =
            await warehouseMerchantRepository.GetFirstBySpecificationAsync(
                WarehouseMerchantSpecs.ById(warehouseMerchantId));

        var uniquenessPolicy = new WarehouseMerchantUniquePreferenceOrderPolicy(existingPreferenceOrders);
        var editValues = updateWarehouseMerchantCommand.ToEditValues();
        existingWarehouseMerchant.Update(editValues, uniquenessPolicy);

        await unitOfWork.SaveChangesAsync();

        return WarehouseMerchantDto.FromEntity(existingWarehouseMerchant);
    }

    public async Task DeleteAsync(DeleteWarehouseMerchantCommand command)
    {
        var warehouseMerchantId = WarehouseMerchantId.Create(command.WarehouseMerchantId);

        var deleteCandidate =
            await warehouseMerchantRepository.GetFirstBySpecificationAsync(
                WarehouseMerchantSpecs.ById(warehouseMerchantId));

        warehouseMerchantRepository.Remove(deleteCandidate);

        await unitOfWork.SaveChangesAsync();
    }
}