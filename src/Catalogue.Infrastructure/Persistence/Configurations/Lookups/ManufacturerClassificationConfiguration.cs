using Catalogue.Domain.Lookups.ManufacturerClassifications;
using Catalogue.Domain.Lookups.ManufacturerClassifications.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ValidationRules.Lookups;

namespace Catalogue.Infrastructure.Persistence.Configurations.Lookups;

public class ManufacturerClassificationConfiguration : IEntityTypeConfiguration<ManufacturerClassification>
{
    public void Configure(EntityTypeBuilder<ManufacturerClassification> entityTypeBuilder)
    {
        {
            entityTypeBuilder.HasKey(mc => mc.Id);

            entityTypeBuilder.ToTable("LU_ManufacturerClassification");

            entityTypeBuilder.HasIndex(mc => mc.Description)
                .HasDatabaseName("uc_ManClass")
                .IsUnique();

            entityTypeBuilder.Property(mc => mc.Id).HasColumnType("int")
                .ValueGeneratedOnAdd()
                .HasConversion(
                    manClassId => manClassId.Value,
                    value => ManufacturerClassificationId.Create(value));

            entityTypeBuilder.Property(mc => mc.Description)
                .IsRequired()
                .HasMaxLength(ManufacturerClassificationValidationRules.DescriptionMaxLength)
                .IsUnicode(false);

            entityTypeBuilder.Property(mc => mc.Active);
        }
    }
}