using Catalogue.Domain.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Catalogue.Infrastructure.Persistence.Configurations.Products;

public partial class ProductConfiguration
{
    private void ConfigureOffers(EntityTypeBuilder<Product> entityTypeBuilder)
    {
        entityTypeBuilder.HasMany(p => p.Offers)
            .WithOne()
            .HasForeignKey(p => p.ProductCode)
            .OnDelete(DeleteBehavior.Cascade);
    }
}