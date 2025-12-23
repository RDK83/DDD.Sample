using Catalogue.Domain.ProductTypes.Entities;
using Catalogue.Domain.ProductTypes.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ValidationRules.ProductTypes;

namespace Catalogue.Infrastructure.Persistence.Configurations.ProductTypes;

public class ProductSubTypeConfiguration : IEntityTypeConfiguration<ProductSubType>
{
    public void Configure(EntityTypeBuilder<ProductSubType> entityTypeBuilder)
    {
        entityTypeBuilder.HasKey(e => e.Id);

        entityTypeBuilder.HasIndex(e => new { e.ProductTypeId, e.ProductSubTypeName })
            .HasDatabaseName("uc_SubType")
            .IsUnique();

        entityTypeBuilder.Property(e => e.Id).HasColumnName("ProductSubTypeID").HasColumnType("int")
            .ValueGeneratedOnAdd()
            .HasConversion(
                productSubTypeId => productSubTypeId.Value,
                value => ProductSubTypeId.Create(value));

        entityTypeBuilder.Property(e => e.ProductSubTypeName).HasColumnName("ProductSubType")
            .IsRequired()
            .HasMaxLength(ProductSubTypeValidationRules.ProductSubTypeNameMaxLength)
            .IsUnicode(false)
            .HasConversion(
                name => name.Value,
                value => ProductSubTypeName.Create(value));

        entityTypeBuilder.Property(e => e.ProductTypeId).HasColumnName("ProductTypeID");
    }
}