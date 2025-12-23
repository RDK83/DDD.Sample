using Catalogue.Domain.Common.ValueObjects.Address;
using Catalogue.Domain.Merchants.ValueObjects;

namespace Catalogue.Domain.Merchants.Mutations;

public record EditMerchantMutation(MerchantName MerchantName, Address Address, bool Active);