using Catalogue.Application.Medias.Responses;
using Shared.Application.Pagination;
using SharedKernel.Pagination;

namespace Catalogue.Application.Medias;

public interface IMediaService
{
    Task<MediaDto> GetByIdAsync(int id);
    Task<OffsetPagedResult<MediaDto>> GetOffSetPagedAsync(PagingQuery paging);
}