using Catalogue.Application.Merchants.Commands;
using Catalogue.Application.Merchants.Queries;
using Catalogue.Application.Merchants.Responses;
using Catalogue.Application.Merchants.Specifications.Helper;
using Catalogue.Application.Shared.Logging;
using Catalogue.Domain.Merchants.Repositories;
using Catalogue.Domain.Merchants.ValueObjects;
using Shared.Application.UnitOfWork;

namespace Catalogue.Application.Merchants;

[LogMethod]
[LogParameters]
public class MerchantService(IMerchantRepository merchantRepository, IUnitOfWork unitOfWork)
    : IMerchantService
{
    public async Task<MerchantDto> GetByIdAsync(GetMerchantByIdQuery query)
    {
        var merchantId = MerchantId.Create(query.MerchantId);
        var merchant = await merchantRepository.GetFirstBySpecificationAsync(MerchantSpecs.ById(merchantId));

        var returnDto = MerchantDto.FromEntity(merchant);

        return returnDto;
    }

    public async Task<MerchantDto> CreateAsync(CreateMerchantCommand command)
    {
        var newMerchant = command.ToEntity();
        merchantRepository.Add(newMerchant);

        await unitOfWork.SaveChangesAsync();

        var returnDto = MerchantDto.FromEntity(newMerchant);
        return returnDto;
    }

    public async Task UpdateAsync(UpdateMerchantCommand command)
    {
        var merchantId = MerchantId.Create(command.MerchantId);
        var existingMerchant = await merchantRepository.GetFirstBySpecificationAsync(MerchantSpecs.ById(merchantId));

        var editMerchantMutation = command.ToMutation();
        existingMerchant.UpdateDetails(editMerchantMutation);

        await unitOfWork.SaveChangesAsync();
    }

    public async Task DeleteAsync(DeleteMerchantCommand command)
    {
        var merchantId = MerchantId.Create(command.MerchantId);
        var existingMerchant = await merchantRepository.GetFirstBySpecificationAsync(MerchantSpecs.ById(merchantId));

        merchantRepository.Remove(existingMerchant);

        await unitOfWork.SaveChangesAsync();
    }
}