using Catalogue.Domain.Common.Enums;
using Catalogue.Domain.Lookups.DeliveryMethods.ValueObjects;
using Catalogue.Domain.Warehouses.Entities;
using Catalogue.Domain.Warehouses.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ValidationRules.Warehouses;

namespace Catalogue.Infrastructure.Persistence.Configurations.Warehouses;

public partial class WarehouseDeliveryMethodConfiguration
{
    public void ConfigureCore(EntityTypeBuilder<WarehouseDeliveryMethod> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd()
            .HasConversion(
                warehouseDeliveryMethodId => warehouseDeliveryMethodId.Value,
                value => WarehouseDeliveryMethodId.Create(value));

        builder.Property(x => x.WarehouseId)
            .HasConversion(
                warehouseId => warehouseId.Value,
                value => WarehouseId.Create(value));

        builder.Property(x => x.DeliveryMethodId).IsRequired()
            .HasColumnType($"char({WarehouseDeliveryMethodValidationRules.DeliveryMethodIdLength})")
            .HasConversion(
                deliveryMethodId => deliveryMethodId.Value,
                value => DeliveryMethodId.Create(value));

        builder.Property(x => x.CountryIsoCode).IsRequired()
            .HasColumnType($"char({WarehouseDeliveryMethodValidationRules.CurrencyIsoCodeLength})")
            .HasConversion(new EnumToStringConverter<Country>());


        builder.ComplexProperty(x => x.LeadTimes, lt =>
        {
            lt.Property(p => p.MinimumLeadTime).HasColumnName("MinLeadTime").HasColumnType("tinyint");
            lt.Property(p => p.MaximumLeadTime).HasColumnName("MaxLeadTime").HasColumnType("tinyint");
        });

        builder.ComplexProperty(x => x.GrossCost, gc =>
        {
            gc.Property(p => p.Currency)
                .HasColumnType($"char({WarehouseDeliveryMethodValidationRules.CurrencyIsoCodeLength})")
                .HasConversion(new EnumToStringConverter<Currency>());
            gc.Property(p => p.Value).HasColumnType("decimal(5, 2)");
        });
    }
}