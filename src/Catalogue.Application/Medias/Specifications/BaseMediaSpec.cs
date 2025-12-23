using Ardalis.Specification;
using Catalogue.Domain.Medias;

namespace Catalogue.Application.Medias.Specifications;

internal class BaseMediaSpec : Specification<Media>
{
    public BaseMediaSpec()
    {
        Query
            .Include(m => m.Type);
    }
}