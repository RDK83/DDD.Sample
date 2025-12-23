using Catalogue.Domain.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Catalogue.Infrastructure.Persistence.Configurations.Products;

public partial class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> entityTypeBuilder)
    {
        ConfigureCore(entityTypeBuilder);
        ConfigureLookups(entityTypeBuilder);
        ConfigureOffers(entityTypeBuilder);
        ConfigureMedia(entityTypeBuilder);
    }
}