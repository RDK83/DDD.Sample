using Ardalis.Specification;
using Catalogue.Domain.Medias;
using Catalogue.Domain.Medias.Repositories;
using Catalogue.Domain.Medias.ValueObjects;
using Catalogue.Infrastructure.Persistence.Repositories.Abstract;
using SharedKernel.Pagination;

namespace Catalogue.Infrastructure.Persistence.Repositories;

public class MediaRepository : Repository<Media, MediaId>, IMediaRepository
{
    public MediaRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
    {
    }

    public Task<OffsetPagedResult<Media>> GetPagedSpecificationAsync(ISpecification<Media> specification, int pageNumber, int pageSize)
    {
        throw new NotImplementedException();
    }

    public void Remove(MediaId mediaId)
    {
        var media = ApplicationDbContext.Set<Media>().Find(mediaId);

        if (media != null)
            ApplicationDbContext.Remove(media);
    }
}