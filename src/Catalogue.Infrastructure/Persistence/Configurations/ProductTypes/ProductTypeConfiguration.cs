using Catalogue.Domain.ProductTypes;
using Catalogue.Domain.ProductTypes.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ValidationRules.ProductTypes;

namespace Catalogue.Infrastructure.Persistence.Configurations.ProductTypes;

public class ProductTypeConfiguration : IEntityTypeConfiguration<ProductType>
{
    public void Configure(EntityTypeBuilder<ProductType> entityTypeBuilder)
    {
        entityTypeBuilder.HasKey(e => e.Id);

        entityTypeBuilder.HasIndex(e => e.ProductTypeName)
            .HasDatabaseName("uc_ProductType")
            .IsUnique();

        entityTypeBuilder.Property(e => e.Id).HasColumnName("ProductTypeID").HasColumnType("int")
            .ValueGeneratedOnAdd()
            .HasConversion(
                productTypeId => productTypeId.Value,
                value => ProductTypeId.Create(value));

        entityTypeBuilder.Property(e => e.ProductTypeName).HasColumnName("ProductType")
            .IsRequired()
            .HasMaxLength(ProductTypeValidationRules.ProductTypeNameMaxLength)
            .IsUnicode(false)
            .HasConversion(
                name => name.Value,
                value => ProductTypeName.Create(value));

        entityTypeBuilder.HasMany(pt => pt.ProductSubTypes)
            .WithOne()
            .HasForeignKey(st => st.ProductTypeId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);
    }
}