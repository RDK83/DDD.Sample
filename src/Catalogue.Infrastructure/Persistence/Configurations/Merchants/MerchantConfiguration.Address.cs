using Catalogue.Domain.Common.Enums;
using Catalogue.Domain.Common.ValueObjects.Address;
using Catalogue.Domain.Merchants;
using Catalogue.Infrastructure.Persistence.ValueConverters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ValidationRules;

namespace Catalogue.Infrastructure.Persistence.Configurations.Merchants;

public partial class MerchantConfiguration
{
    public virtual void ConfigureAddress(EntityTypeBuilder<Merchant> entityTypeBuilder)
    {
        entityTypeBuilder.ComplexProperty(merchant => merchant.Address, addressBuilder =>
        {
            addressBuilder.Property(x => x.AddressLine1)
                .HasColumnType($"nvarchar({AddressValidationRules.AddressLineMaxLength})")
                .IsRequired().ValueGeneratedNever().HasMaxLength(AddressValidationRules.AddressLineMaxLength)
                .HasConversion(addressLine1 => addressLine1.Value,
                    value => PrimaryAddressLine.Create(value));


            addressBuilder.Property(x => x.AddressLine2)
                .HasColumnType($"nvarchar({AddressValidationRules.AddressLineMaxLength})")
                .ValueGeneratedNever().HasMaxLength(AddressValidationRules.AddressLineMaxLength)
                .IsRequired(false)
                .HasConversion(new NullableStringValueObjectValueConverter<AddressLine>(AddressLine.Create));

            addressBuilder.Property(x => x.AddressLine3)
                .HasColumnType($"nvarchar({AddressValidationRules.AddressLineMaxLength})")
                .ValueGeneratedNever().HasMaxLength(AddressValidationRules.AddressLineMaxLength)
                .IsRequired(false)
                .HasConversion(new NullableStringValueObjectValueConverter<AddressLine>(AddressLine.Create));

            addressBuilder.Property(x => x.AddressLine4)
                .HasColumnType($"nvarchar({AddressValidationRules.AddressLineMaxLength})")
                .ValueGeneratedNever().HasMaxLength(AddressValidationRules.AddressLineMaxLength)
                .IsRequired(false)
                .HasConversion(new NullableStringValueObjectValueConverter<AddressLine>(AddressLine.Create));

            addressBuilder.Property(x => x.City)
                .HasColumnType($"nvarchar({AddressValidationRules.CityMaxLength})")
                .IsRequired().ValueGeneratedNever().HasMaxLength(AddressValidationRules.CityMaxLength)
                .HasConversion(city => city.Value,
                    value => City.Create(value));

            addressBuilder.Property(x => x.County)
                .HasColumnType($"varchar({AddressValidationRules.CountyMaxLength})")
                .ValueGeneratedNever().HasMaxLength(AddressValidationRules.CountyMaxLength)
                .IsRequired(false)
                .HasConversion(new NullableStringValueObjectValueConverter<County>(County.Create));

            addressBuilder.ComplexProperty(address => address.PostalCode, builder =>
            {
                builder.Property(postalCode => postalCode.Postcode)
                    .HasColumnType(@"nvarchar(50)")
                    .IsRequired().ValueGeneratedNever().HasMaxLength(AddressValidationRules.PostCodeMaxLength);
                builder.Property(postalCode => postalCode.CountryId)
                    .HasColumnType(@"char(2)").IsRequired().ValueGeneratedNever()
                    .HasMaxLength(AddressValidationRules.CountryCodeLength)
                    .HasConversion(new EnumToStringConverter<Country>());
            });
        });
    }
}