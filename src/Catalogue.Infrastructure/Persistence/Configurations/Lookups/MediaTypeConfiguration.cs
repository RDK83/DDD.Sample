using Catalogue.Domain.Lookups.MediaTypes;
using Catalogue.Domain.Lookups.MediaTypes.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ValidationRules.Lookups;

namespace Catalogue.Infrastructure.Persistence.Configurations.Lookups;

internal class MediaTypeConfiguration : IEntityTypeConfiguration<MediaType>
{
    public void Configure(EntityTypeBuilder<MediaType> builder)
    {
        builder.HasKey(e => e.Id);

        builder.ToTable("LU_MediaType");

        builder.Property(e => e.Id).HasColumnType("tinyint")
            .ValueGeneratedOnAdd()
            .HasConversion(id => id.Value,
                value => MediaTypeId.Create(value));

        builder.Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(MediaTypeValidationRules.NameMaxLength)
            .IsUnicode(false);
    }
}