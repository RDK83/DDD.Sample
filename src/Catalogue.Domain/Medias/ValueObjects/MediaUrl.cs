using Shared.Domain.Aggregates.ValueObjects;
using Shared.Domain.Guard;
using ValidationRules.Media;

namespace Catalogue.Domain.Medias.ValueObjects;

public record MediaUrl : IStringValueObject<MediaUrl>
{
    public string Value { get; }
    public static int MaxLength => MediaValidationRules.MediaUrlMaxLength;

    public MediaUrl(string value)
    {
        Value = value;
    }

    public static MediaUrl Create(string input)
    {
        Guard.Against.NullOrWhiteSpace(input);

        input = input.Trim();

        Guard.Against.StringTooLong(input, MaxLength);

        return new MediaUrl(input);
    }

    public static implicit operator string(MediaUrl vo)
    {
        return vo.Value;
    }
}