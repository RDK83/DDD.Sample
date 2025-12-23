using Ardalis.Specification;
using Catalogue.Domain.Medias.ValueObjects;

namespace Catalogue.Application.Medias.Specifications;

internal class MediaByIdSpec : BaseMediaSpec
{
    internal MediaByIdSpec(MediaId id)
    {
        Query.Where(m => m.Id == id);
    }
}