using Shared.Domain.Aggregates.ValueObjects;
using ValidationRules.Media;

namespace Catalogue.Domain.Medias.ValueObjects;

public record MediaAltText : INullableStringValueObject<MediaAltText>
{
    public string? Value { get; }

    public static int MaxLength => MediaValidationRules.MediaAltTextMaxLength;

    public MediaAltText(string? value)
    {
        Value = value;
    }

    public static MediaAltText Create(string? input)
    {
        if (input is null)
            return new MediaAltText(input);

        input = input.Trim();

        return new MediaAltText(input);
    }

    public static implicit operator string?(MediaAltText vo)
    {
        return vo.Value;
    }
}