using Catalogue.Domain.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Catalogue.Infrastructure.Persistence.Configurations.Products;

public partial class ProductConfiguration
{
    private void ConfigureLookups(EntityTypeBuilder<Product> entityTypeBuilder)
    {
        entityTypeBuilder.HasOne(p => p.ManufacturerClassification)
            .WithMany()
            .HasForeignKey(p => p.ManufacturerClassificationId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_SF_Products_SF_LU_ManufacturerClassification");
    }
}