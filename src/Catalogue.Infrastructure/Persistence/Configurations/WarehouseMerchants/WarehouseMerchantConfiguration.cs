using Catalogue.Domain.Merchants.ValueObjects;
using Catalogue.Domain.WarehouseMerchants;
using Catalogue.Domain.WarehouseMerchants.ValueObjects;
using Catalogue.Domain.Warehouses.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Catalogue.Infrastructure.Persistence.Configurations.WarehouseMerchants;

public class WarehouseMerchantConfiguration : IEntityTypeConfiguration<WarehouseMerchant>
{
    public void Configure(EntityTypeBuilder<WarehouseMerchant> entityTypeBuilder)
    {
        entityTypeBuilder.HasKey(wm => wm.Id);

        entityTypeBuilder.HasIndex(wm => wm.PreferenceOrder).IsUnique();

        entityTypeBuilder.Property(x => x.Id).HasColumnType("int").IsRequired()
            .ValueGeneratedOnAdd()
            .HasConversion(
                merchantId => merchantId.Value,
                value => WarehouseMerchantId.Create(value));

        entityTypeBuilder.Property(x => x.WarehouseId).HasColumnType("int").IsRequired()
            .ValueGeneratedNever()
            .HasConversion(
                warehouseId => warehouseId.Value,
                value => WarehouseId.Create(value));

        entityTypeBuilder.Property(x => x.MerchantId).HasColumnType("int").IsRequired()
            .ValueGeneratedNever()
            .HasConversion(
                merchantId => merchantId.Value,
                value => MerchantId.Create(value));

        entityTypeBuilder.Property(wm => wm.Active).HasColumnType("bit").IsRequired().ValueGeneratedNever();

        entityTypeBuilder.Property(wm => wm.PreferenceOrder).HasColumnType("int").IsRequired().ValueGeneratedNever()
            .HasConversion(
                preferenceOrder => preferenceOrder.Value,
                value => PreferenceOrder.Create(value));
        entityTypeBuilder.HasIndex(x => x.PreferenceOrder).IsUnique();
    }
}