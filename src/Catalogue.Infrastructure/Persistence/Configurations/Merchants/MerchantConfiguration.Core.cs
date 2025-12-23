using Catalogue.Domain.Merchants;
using Catalogue.Domain.Merchants.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ValidationRules.Merchants;

namespace Catalogue.Infrastructure.Persistence.Configurations.Merchants;

public partial class MerchantConfiguration
{
    public virtual void ConfigureCore(EntityTypeBuilder<Merchant> entityTypeBuilder)
    {
        entityTypeBuilder.HasKey(x => x.Id);

        entityTypeBuilder.HasIndex(e => e.MerchantCode).IsUnique();

        entityTypeBuilder.Property(x => x.Id).HasColumnType("int").IsRequired()
            .ValueGeneratedOnAdd()
            .HasConversion(
                merchantId => merchantId.Value,
                value => MerchantId.Create(value));

        entityTypeBuilder.Property(x => x.MerchantName)
            .HasColumnType($"nvarchar({MerchantValidationRules.MerchantNameMaxLength})").IsRequired()
            .ValueGeneratedNever().HasMaxLength(MerchantValidationRules.MerchantNameMaxLength)
            .HasConversion(
                merchantName => merchantName.Value,
                value => MerchantName.Create(value));

        entityTypeBuilder.Property(x => x.MerchantCode)
            .HasColumnType($"nvarchar({MerchantValidationRules.MerchantCodeMaxLength})").IsRequired()
            .ValueGeneratedNever().HasMaxLength(MerchantValidationRules.MerchantCodeMaxLength)
            .HasConversion(
                merchantName => merchantName.Value,
                value => MerchantCode.Create(value));

        entityTypeBuilder.Property(x => x.Active).HasColumnName("Active").HasColumnType("bit").IsRequired()
            .ValueGeneratedNever();
    }
}