using Shared.Domain.Aggregates.ValueObjects;
using Shared.Domain.Guard;

namespace Catalogue.Domain.Lookups.MediaTypes.ValueObjects;

public readonly record struct MediaTypeId : IByteValueObject<MediaTypeId>
{
    public byte Value { get; }

    private MediaTypeId(byte value)
    {
        Value = value;
    }

    public static MediaTypeId Create(byte input)
    {
        Guard.Against.Negative(input);

        return new MediaTypeId(input);
    }

    public static implicit operator byte(MediaTypeId vo)
    {
        return vo.Value;
    }
}