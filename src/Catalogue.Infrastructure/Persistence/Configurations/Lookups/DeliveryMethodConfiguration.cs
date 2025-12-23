using Catalogue.Domain.Lookups.DeliveryMethods;
using Catalogue.Domain.Lookups.DeliveryMethods.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ValidationRules.Lookups;

namespace Catalogue.Infrastructure.Persistence.Configurations.Lookups;

internal class DeliveryMethodConfiguration : IEntityTypeConfiguration<DeliveryMethod>
{
    public void Configure(EntityTypeBuilder<DeliveryMethod> entityTypeBuilder)
    {
        entityTypeBuilder.HasKey(e => e.Id);

        entityTypeBuilder.ToTable("LU_DeliveryMethod");

        entityTypeBuilder.Property(e => e.Id)
            .HasColumnType($"char({DeliveryMethodValidationRules.DeliveryMethodIdLength})")
            .ValueGeneratedNever()
            .HasConversion(deliveryMethodId => deliveryMethodId.Value,
                value => DeliveryMethodId.Create(value));

        entityTypeBuilder.Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(DeliveryMethodValidationRules.DeliveryMethodNameMaxLength);
    }
}