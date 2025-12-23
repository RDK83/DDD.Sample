using Catalogue.Domain.Warehouses;
using Catalogue.Domain.Warehouses.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ValidationRules.Warehouses;

namespace Catalogue.Infrastructure.Persistence.Configurations.Warehouses;

public partial class WarehouseConfiguration
{
    public virtual void ConfigureCore(EntityTypeBuilder<Warehouse> entityTypeBuilder)
    {
        entityTypeBuilder.HasKey(x => x.Id);

        entityTypeBuilder.Property(x => x.Id).HasColumnType(@"int").IsRequired()
            .ValueGeneratedOnAdd()
            .HasConversion(
                warehouseId => warehouseId.Value,
                value => WarehouseId.Create(value));

        entityTypeBuilder.Property(x => x.WarehouseName)
            .HasColumnType($"nvarchar({WarehouseValidationRules.WarehouseNameMaxLength})").IsRequired()
            .ValueGeneratedNever()
            .HasConversion(
                warehouseName => warehouseName.Value,
                value => WarehouseName.Create(value));

        entityTypeBuilder.Property(x => x.WarehouseCode)
            .HasColumnType($"varchar({WarehouseValidationRules.WarehouseCodeMaxLength})").IsRequired()
            .ValueGeneratedNever()
            .HasConversion(
                warehouseCode => warehouseCode.Value,
                value => WarehouseCode.Create(value));
        entityTypeBuilder.HasIndex(x => x.WarehouseCode).IsUnique();

        entityTypeBuilder.Property(x => x.Active).HasColumnType("bit").IsRequired().ValueGeneratedNever();
    }
}