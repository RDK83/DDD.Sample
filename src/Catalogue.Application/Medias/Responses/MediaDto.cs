using Catalogue.Domain.Medias;

namespace Catalogue.Application.Medias.Responses;

public record MediaDto(int Id, int TypeId, string Url, string? AltText)
{
    public static MediaDto FromEntity(Media entity)
    {
        var dto = new MediaDto(
            Id: entity.Id,
            TypeId: entity.TypeId,
            Url: entity.Url,
            AltText: entity.AltText
        );

        return dto;
    }
}