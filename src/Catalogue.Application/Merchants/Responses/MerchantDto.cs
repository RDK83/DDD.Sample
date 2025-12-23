using Catalogue.Domain.Merchants;

namespace Catalogue.Application.Merchants.Responses;

public record MerchantDto(
    int Id,
    string MerchantName,
    string MerchantCode,
    string AddressLine1,
    string City,
    string CountryIsoCode,
    string Postcode,
    bool Active,
    string? AddressLine2,
    string? AddressLine3,
    string? AddressLine4,
    string? County)
{
    public static MerchantDto FromEntity(Merchant entity)
    {
        return new MerchantDto
        (
            Id: entity.Id,
            MerchantName: entity.MerchantName,
            MerchantCode: entity.MerchantCode,
            AddressLine1: entity.Address.AddressLine1,
            City: entity.Address.City,
            CountryIsoCode: entity.Address.PostalCode.CountryId.ToString(),
            Postcode: entity.Address.PostalCode.Postcode,
            Active: entity.Active,
            AddressLine2: entity.Address.AddressLine2,
            AddressLine3: entity.Address.AddressLine3,
            AddressLine4: entity.Address.AddressLine4,
            County: entity.Address.County
        );
    }
}