using Catalogue.Domain.Common.Enums;
using Catalogue.Domain.Common.ValueObjects.Address;
using Catalogue.Domain.Merchants;
using Catalogue.Domain.Merchants.Mutations;
using Catalogue.Domain.Merchants.ValueObjects;

namespace Catalogue.Domain.UnitTests.Merchants;

public class MerchantTestHelper
{
    public static Merchant CreateValidMerchant()
    {
        var address = CreateValidMerchantAddress();
        return Merchant.Create(
            MerchantName.Create("Test Valid Merchant"),
            MerchantCode.Create("TVM"),
            address,
            true);
    }

    public static Merchant CreateInvalidMerchant()
    {
        var address = CreateValidMerchantAddress();
        return Merchant.Create(
            MerchantName.Create(""),
            MerchantCode.Create("TVM"),
            address,
            true);
    }

    public static EditMerchantMutation CreateValidEditMerchantMutation()
    {
        var newAddress = CreateValidAlternateMerchantAddress();
        return new EditMerchantMutation(
            MerchantName.Create("Test Valid Merchant Altered"),
            newAddress,
            false);
    }

    private static Address CreateValidMerchantAddress()
    {
        return Address.Create(
            PrimaryAddressLine.Create("Unit 1"),
            AddressLine.Create("Fairs Park"),
            AddressLine.Create("University Way"),
            AddressLine.Create("-"),
            City.Create("Birmingham"),
            County.Create("West Midlands"),
            PostalCode.Create("B1 6NG", Country.GB));
    }

    private static Address CreateValidAlternateMerchantAddress()
    {
        return Address.Create(
            PrimaryAddressLine.Create("Road One"),
            AddressLine.Create("Birmingham Industrial Estate"),
            AddressLine.Create("Turtle Way"),
            AddressLine.Create("Sample Address Line 4"),
            City.Create("Birmingham"),
            County.Create("West Midlands Still"),
            PostalCode.Create("B7 3QA", Country.GB));
    }
}