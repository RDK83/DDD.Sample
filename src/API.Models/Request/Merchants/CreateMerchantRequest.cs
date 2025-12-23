using System.ComponentModel.DataAnnotations;
using ValidationRules.Merchants;

namespace API.Models.Request.Merchants;

public record CreateMerchantRequest
{
    [MaxLength(MerchantValidationRules.MerchantNameMaxLength)]
    public required string MerchantName { get; init; }

    [MaxLength(MerchantValidationRules.MerchantCodeMaxLength)]
    public required string MerchantCode { get; init; }

    [MaxLength(MerchantValidationRules.AddressLineMaxLength)]
    public required string AddressLine1 { get; init; }

    [MinLength(MerchantValidationRules.CountryIsoCodeLength)]
    [MaxLength(MerchantValidationRules.CountryIsoCodeLength)]
    public required string CountryIsoCode { get; init; }

    [MaxLength(MerchantValidationRules.CityMaxLength)]
    public required string City { get; init; }

    [MaxLength(MerchantValidationRules.PostCodeMaxLength)]
    public required string PostCode { get; init; }

    public required bool Active { get; init; }

    [MaxLength(MerchantValidationRules.AddressLineMaxLength)]
    public string? AddressLine2 { get; init; }

    [MaxLength(MerchantValidationRules.AddressLineMaxLength)]
    public string? AddressLine3 { get; init; }

    [MaxLength(MerchantValidationRules.AddressLineMaxLength)]
    public string? AddressLine4 { get; init; }

    [MaxLength(MerchantValidationRules.CountyMaxLength)]
    public string? County { get; init; }
}