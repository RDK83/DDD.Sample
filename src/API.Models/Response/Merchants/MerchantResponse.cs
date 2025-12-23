namespace API.Models.Response.Merchants;

public record MerchantResponse(
    string Id,
    string MerchantName,
    string MerchantCode,
    string AddressLine1,
    string City,
    string Postcode,
    string CountryIsoCode,
    bool Active,
    string? AddressLine2,
    string? AddressLine3,
    string? AddressLine4,
    string? County);