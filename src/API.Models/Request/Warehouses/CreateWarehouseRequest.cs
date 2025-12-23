using System.ComponentModel.DataAnnotations;
using ValidationRules.Warehouses;

namespace API.Models.Request.Warehouses;

public record CreateWarehouseRequest
{
    [MaxLength(WarehouseValidationRules.WarehouseNameMaxLength)]
    public required string WarehouseName { get; init; }

    [MaxLength(WarehouseValidationRules.WarehouseCodeMaxLength)]
    public required string WarehouseCode { get; init; }

    [MaxLength(WarehouseValidationRules.AddressLineMaxLength)]
    public required string AddressLine1 { get; init; }

    [MaxLength(WarehouseValidationRules.CityMaxLength)]
    public required string City { get; init; }

    [MaxLength(WarehouseValidationRules.PostCodeMaxLength)]
    public required string PostCode { get; init; }

    [MinLength(WarehouseValidationRules.CountryIsCodeLength)]
    [MaxLength(WarehouseValidationRules.CountryIsCodeLength)]
    public required string CountryIsoCode { get; init; }


    public required bool Active { get; init; }

    [MaxLength(WarehouseValidationRules.AddressLineMaxLength)]
    public string? AddressLine2 { get; init; }

    [MaxLength(WarehouseValidationRules.AddressLineMaxLength)]
    public string? AddressLine3 { get; init; }

    [MaxLength(WarehouseValidationRules.AddressLineMaxLength)]
    public string? AddressLine4 { get; init; }

    [MaxLength(WarehouseValidationRules.CountyMaxLength)]
    public string? County { get; init; }
}