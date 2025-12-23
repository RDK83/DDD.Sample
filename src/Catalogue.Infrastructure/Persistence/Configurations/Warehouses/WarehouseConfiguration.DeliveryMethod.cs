using Catalogue.Domain.Warehouses;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Catalogue.Infrastructure.Persistence.Configurations.Warehouses;

public partial class WarehouseConfiguration
{
    public virtual void ConfigureDeliveryMethods(EntityTypeBuilder<Warehouse> entityTypeBuilder)
    {
        entityTypeBuilder.HasMany(w => w.WarehouseDeliveryMethods)
            .WithOne()
            .HasForeignKey(wdm => wdm.WarehouseId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}