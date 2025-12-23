using Catalogue.Application.Medias.Responses;
using Catalogue.Application.Medias.Specifications;
using Catalogue.Application.Shared.Logging;
using Catalogue.Domain.Medias.Repositories;
using Catalogue.Domain.Medias.ValueObjects;
using Shared.Application.Pagination;
using SharedKernel.Pagination;

namespace Catalogue.Application.Medias;

[LogMethod]
[LogParameters]
public class MediaService : IMediaService
{
    private readonly IMediaRepository _mediaRepository;

    public MediaService(IMediaRepository mediaRepository)
    {
        _mediaRepository = mediaRepository;
    }

    public async Task<MediaDto> GetByIdAsync(int id)
    {
        var entity = await _mediaRepository.GetFirstBySpecificationAsync(new MediaByIdSpec(MediaId.Create(id)));

        return MediaDto.FromEntity(entity);
    }

    public async Task<OffsetPagedResult<MediaDto>> GetOffSetPagedAsync(PagingQuery paging)
    {
        var spec = new BaseMediaSpec();

        var pagedResult =
            await _mediaRepository.GetOffsetPagedBySpecificationAsync(spec, paging.PageNumber, paging.PageSize);

        var medias = pagedResult.Items.Select(MediaDto.FromEntity).ToList().AsReadOnly();

        return new OffsetPagedResult<MediaDto>(medias, pagedResult.TotalCount, pagedResult.PageNumber,
            pagedResult.PageSize);
    }
}