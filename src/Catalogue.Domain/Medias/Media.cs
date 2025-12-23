using Catalogue.Domain.Lookups.MediaTypes;
using Catalogue.Domain.Lookups.MediaTypes.ValueObjects;
using Catalogue.Domain.Medias.ValueObjects;
using Shared.Domain.Aggregates;

namespace Catalogue.Domain.Medias;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
public class Media : BaseAggregate<MediaId>
{
    public MediaTypeId TypeId { get; }

    public MediaUrl Url { get; private set; }

    public MediaAltText AltText { get; private set; }

    public MediaType Type { get; private set; }


    private Media()
    {
        // EF required
    }

    private Media(MediaTypeId typeId, MediaUrl url, MediaAltText altText)
    {
        TypeId = typeId;
        Url = url;
        AltText = altText;
    }

    public static Media Create(MediaTypeId typeId, MediaUrl url, MediaAltText altText)
    {
        return new Media(
            typeId, url, altText
        );
    }
}