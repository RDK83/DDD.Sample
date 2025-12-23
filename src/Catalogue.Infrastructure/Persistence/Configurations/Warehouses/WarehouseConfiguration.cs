using Catalogue.Domain.Warehouses;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Catalogue.Infrastructure.Persistence.Configurations.Warehouses;

public partial class WarehouseConfiguration : IEntityTypeConfiguration<Warehouse>
{
    public void Configure(EntityTypeBuilder<Warehouse> entityTypeBuilder)
    {
        ConfigureCore(entityTypeBuilder);
        ConfigureAddress(entityTypeBuilder);
        ConfigureDeliveryMethods(entityTypeBuilder);
    }
}