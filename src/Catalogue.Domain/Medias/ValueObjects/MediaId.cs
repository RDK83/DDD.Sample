using Shared.Domain.Aggregates.ValueObjects;
using Shared.Domain.Guard;

namespace Catalogue.Domain.Medias.ValueObjects;

public readonly record struct MediaId : IIntegerValueObject<MediaId>
{
    public int Value { get; }

    private MediaId(int value)
    {
        Value = value;
    }

    public static MediaId Create(int input)
    {
        DomainGuard.AgainstZero(input);

        return new MediaId(input);
    }

    public static implicit operator int(MediaId vo)
    {
        return vo.Value;
    }
}