using Catalogue.Domain.Medias.ValueObjects;
using Catalogue.Domain.Products.Entities;
using Catalogue.Domain.Products.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ValidationRules.Products;

namespace Catalogue.Infrastructure.Persistence.Configurations.Products;

internal class ProductMediaConfiguration : IEntityTypeConfiguration<ProductMedia>
{
    public void Configure(EntityTypeBuilder<ProductMedia> entityTypeBuilder)
    {
        entityTypeBuilder.HasKey(productMedia => new { productMedia.ProductCode, productMedia.MediaId });

        entityTypeBuilder.Property(e => e.ProductCode)
            .HasMaxLength(ProductMediaValidationRules.ProductCodeMaxLength)
            .IsUnicode(false)
            .HasConversion(productCode => productCode.Value,
                value => ProductCode.Create(value));


        entityTypeBuilder.Property(e => e.MediaId).HasColumnName("MediaID")
            .HasConversion(mediaId => mediaId.Value,
                value => MediaId.Create(value));
    }
}