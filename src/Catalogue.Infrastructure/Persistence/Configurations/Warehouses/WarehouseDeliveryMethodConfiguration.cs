using Catalogue.Domain.Warehouses.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Catalogue.Infrastructure.Persistence.Configurations.Warehouses;

public partial class WarehouseDeliveryMethodConfiguration : IEntityTypeConfiguration<WarehouseDeliveryMethod>
{
    public void Configure(EntityTypeBuilder<WarehouseDeliveryMethod> builder)
    {
        ConfigureCore(builder);
        ConfigureLookups(builder);
    }
}