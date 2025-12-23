using Catalogue.Domain.Lookups.ManufacturerClassifications.ValueObjects;
using Catalogue.Domain.Products;
using Catalogue.Domain.Products.ValueObjects;
using Catalogue.Domain.ProductTypes.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ValidationRules.Products;

namespace Catalogue.Infrastructure.Persistence.Configurations.Products;

public partial class ProductConfiguration
{
    public void ConfigureCore(EntityTypeBuilder<Product> entityTypeBuilder)
    {
        entityTypeBuilder.HasKey(p => p.Id);

        entityTypeBuilder.Property(p => p.Id).HasColumnName("Id")
            .HasColumnType($"varchar({ProductValidationRules.ProductCodeMaxLength})")
            .HasMaxLength(ProductValidationRules.ProductCodeMaxLength)
            .ValueGeneratedNever()
            .IsUnicode(false)
            .HasConversion(
                productCode => productCode.Value,
                value => ProductCode.Create(value));

        entityTypeBuilder.Ignore(p => p.PromotedOfferId);

        entityTypeBuilder.Property(p => p.TaxClass).HasColumnType("tinyint");

        entityTypeBuilder.Property(p => p.ProductTitle)
            .HasMaxLength(ProductValidationRules.TitleMaxLength)
            .HasConversion(productTitle => productTitle.Value,
                value => ProductTitle.Create(value));

        entityTypeBuilder.Property(p => p.Description)
            .HasMaxLength(ProductValidationRules.DescriptionMaxLength)
            .HasConversion(description => description.Value,
                value => ProductDescription.Create(value));

        entityTypeBuilder.Property<DateTime>("_updatedAt").HasColumnName("LastUpdated").HasColumnType("datetime")
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .IsRequired();

        entityTypeBuilder.Property(p => p.ManufacturerClassificationId).HasColumnName("ManufacturerClassificationID")
            .HasColumnType("int")
            .IsRequired()
            .HasConversion(
                manClassId => manClassId.Value,
                value => ManufacturerClassificationId.Create(value));

        entityTypeBuilder.Property(p => p.ProductTypeId).HasColumnName("ProductTypeID").HasColumnType("int")
            .IsRequired()
            .HasConversion(
                productTypeId => productTypeId.Value,
                value => ProductTypeId.Create(value));

        entityTypeBuilder.Property(p => p.ProductSubTypeId).HasColumnName("ProductSubTypeID").HasColumnType("int")
            .IsRequired()
            .HasConversion(
                productSubTypeId => productSubTypeId.Value,
                value => ProductSubTypeId.Create(value));
    }
}