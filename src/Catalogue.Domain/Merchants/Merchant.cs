using Catalogue.Domain.Common.ValueObjects.Address;
using Catalogue.Domain.Merchants.Mutations;
using Catalogue.Domain.Merchants.ValueObjects;
using Shared.Domain.Aggregates;
using Shared.Domain.Guard;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
namespace Catalogue.Domain.Merchants;

public class Merchant : BaseAggregate<MerchantId>
{
    public MerchantName MerchantName { get; private set; }

    public MerchantCode MerchantCode { get; private set; }

    public Address Address { get; private set; }

    public bool Active { get; private set; }

    private Merchant()
    {
        //EF
    }

    protected Merchant(MerchantName merchantName, MerchantCode merchantCode, Address address, bool active)
    {
        MerchantName = merchantName;
        MerchantCode = merchantCode;
        Address = address;
        Active = active;
    }

    public static Merchant Create(MerchantName merchantName, MerchantCode merchantCode, Address address, bool active)
    {
        return new Merchant(merchantName, merchantCode, address, active);
    }

    public void UpdateDetails(EditMerchantMutation mutation)
    {
        DomainGuard.AgainstNull(mutation);

        MerchantName = mutation.MerchantName;
        Active = mutation.Active;

        Address = mutation.Address;
    }
}