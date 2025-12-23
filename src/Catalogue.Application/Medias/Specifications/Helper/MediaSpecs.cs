using Ardalis.Specification;
using Catalogue.Domain.Medias;
using Catalogue.Domain.Medias.ValueObjects;

namespace Catalogue.Application.Medias.Specifications.Helper;

internal static class MediaSpecs
{
    internal static ISpecification<Media> ById(MediaId mediaId) => new MediaByIdSpec(mediaId);
}