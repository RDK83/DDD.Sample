using Catalogue.Domain.Common.Enums;
using Catalogue.Domain.Merchants.ValueObjects;
using Catalogue.Domain.Products.Entities;
using Catalogue.Domain.Products.ValueObjects;
using Catalogue.Domain.Warehouses.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ValidationRules.Products;

namespace Catalogue.Infrastructure.Persistence.Configurations.Products;

public class OfferConfiguration : IEntityTypeConfiguration<Offer>
{
    public void Configure(EntityTypeBuilder<Offer> offerBuilder)
    {
        offerBuilder.HasKey(e => e.Id);

        offerBuilder.Property(x => x.Id).HasColumnType("int")
            .IsRequired()
            .ValueGeneratedOnAdd()
            .HasConversion(warehouseMerchantProductId => warehouseMerchantProductId.Value,
                value => OfferId.Create(value));

        offerBuilder.Property(x => x.WarehouseId).HasColumnType("int").IsRequired()
            .ValueGeneratedNever()
            .HasConversion(warehouseId => warehouseId.Value,
                value => WarehouseId.Create(value));

        offerBuilder.Property(x => x.MerchantId).HasColumnType("int").IsRequired()
            .ValueGeneratedNever()
            .HasConversion(merchantId => merchantId.Value,
                value => MerchantId.Create(value));

        offerBuilder.Property(x => x.ProductCode)
            .HasColumnType($"varchar({OfferValidationRules.ProductCodeMaxLength})")
            .IsRequired().ValueGeneratedNever().HasMaxLength(OfferValidationRules.ProductCodeMaxLength)
            .HasConversion(productCode => productCode.Value,
                value => ProductCode.Create(value));

        offerBuilder.ComplexProperty(x => x.GrossPrice, gpBuilder =>
        {
            gpBuilder.Property(p => p.Currency)
                .HasColumnType($"char({OfferValidationRules.CurrencyIsoCodeLength})")
                .HasConversion(new EnumToStringConverter<Currency>());
            gpBuilder.Property(p => p.Value).HasColumnName("GrossPrice").HasColumnType("money");
        });

        offerBuilder.Property(x => x.StockLevel).HasColumnType("int").IsRequired()
            .ValueGeneratedNever()
            .HasConversion(stockLevel => stockLevel.Value,
                value => StockLevel.Create(value));
    }
}