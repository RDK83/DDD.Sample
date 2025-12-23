using Catalogue.Domain.Warehouses.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Catalogue.Infrastructure.Persistence.Configurations.Warehouses;

public partial class WarehouseDeliveryMethodConfiguration
{
    public void ConfigureLookups(EntityTypeBuilder<WarehouseDeliveryMethod> builder)
    {
        builder.HasOne(wdm => wdm.DeliveryMethod)
            .WithMany()
            .HasForeignKey(wdm => wdm.DeliveryMethodId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_DL_WarehouseDeliveryMethod_LU_DeliveryMethod");
    }
}