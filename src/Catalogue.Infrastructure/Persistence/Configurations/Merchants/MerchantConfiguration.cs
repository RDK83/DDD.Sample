using Catalogue.Domain.Merchants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Catalogue.Infrastructure.Persistence.Configurations.Merchants;

public partial class MerchantConfiguration : IEntityTypeConfiguration<Merchant>
{
    public void Configure(EntityTypeBuilder<Merchant> entityTypeBuilder)
    {
        ConfigureCore(entityTypeBuilder);
        ConfigureAddress(entityTypeBuilder);
    }
}