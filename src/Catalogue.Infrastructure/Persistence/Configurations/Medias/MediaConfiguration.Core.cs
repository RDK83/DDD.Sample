using Catalogue.Domain.Lookups.MediaTypes.ValueObjects;
using Catalogue.Domain.Medias;
using Catalogue.Domain.Medias.ValueObjects;
using Catalogue.Infrastructure.Persistence.ValueConverters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ValidationRules.Lookups;

namespace Catalogue.Infrastructure.Persistence.Configurations.Medias;

internal partial class MediaConfiguration
{
    public void ConfigureCore(EntityTypeBuilder<Media> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id).HasColumnType("int")
            .ValueGeneratedOnAdd()
            .HasConversion(id => id.Value,
                value => MediaId.Create(value));

        builder.Property(e => e.AltText).HasMaxLength(MediaTypeValidationRules.AltTextMaxLength).IsRequired(false)
            .HasConversion(new NullableStringValueObjectValueConverter<MediaAltText>(MediaAltText.Create));


        builder.Property(e => e.TypeId).HasColumnType("tinyint")
            .HasConversion(typeId => typeId.Value,
                value => MediaTypeId.Create(value));

        builder.Property(e => e.Url)
            .IsRequired()
            .HasMaxLength(MediaTypeValidationRules.UrlMaxLength)
            .HasConversion(mediaUrl => mediaUrl.Value,
                value => MediaUrl.Create(value));
    }
}