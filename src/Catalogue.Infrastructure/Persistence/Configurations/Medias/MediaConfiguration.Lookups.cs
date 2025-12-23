using Catalogue.Domain.Medias;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Catalogue.Infrastructure.Persistence.Configurations.Medias;

internal partial class MediaConfiguration
{
    public void ConfigureLookups(EntityTypeBuilder<Media> builder)
    {
        builder.HasOne(d => d.Type)
            .WithMany()
            .HasForeignKey(d => d.TypeId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_SF_MS_MediaStorage_SF_MS_MediaType");
    }
}