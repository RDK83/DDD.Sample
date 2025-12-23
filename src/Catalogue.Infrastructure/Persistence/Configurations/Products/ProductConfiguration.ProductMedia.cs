using Catalogue.Domain.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Catalogue.Infrastructure.Persistence.Configurations.Products;

public partial class ProductConfiguration
{
    public void ConfigureMedia(EntityTypeBuilder<Product> entityTypeBuilder)
    {
        entityTypeBuilder.HasMany(p => p.ProductMedias)
            .WithOne()
            .HasForeignKey(pm => pm.ProductCode)
            .OnDelete(DeleteBehavior.Cascade);
    }
}