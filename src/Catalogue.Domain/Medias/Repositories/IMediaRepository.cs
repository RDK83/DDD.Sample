using Catalogue.Domain.Medias.ValueObjects;
using SharedKernel.Repositories;

namespace Catalogue.Domain.Medias.Repositories;

public interface IMediaRepository : IRepository<Media>, IMediaReadRepository
{
    public void Remove(MediaId mediaId);
}