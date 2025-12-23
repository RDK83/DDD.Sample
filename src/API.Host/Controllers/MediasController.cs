using API.Host.Controllers.Abstract;
using API.Models.Pagination;
using API.Models.Response.Medias;
using API.Models.RouteValues.Media;
using Catalogue.Application.Medias;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Application.Pagination;

namespace API.Host.Controllers;

[Authorize]
[ApiController]
[Route("[controller]/{mediaId}")]
public class MediasController : ControllerBaseExtended<MediasController, MediaRouteValues, MediaResponse>,
    IGetByIdController<MediaRouteValues, MediaResponse>
{
    private readonly IMediaService _mediaService;

    public MediasController(IMediaService mediaService)
    {
        _mediaService = mediaService;
    }

    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpGet]
    public async Task<ActionResult<MediaResponse>> GetById(MediaRouteValues routeValues)
    {
        var media = await _mediaService.GetByIdAsync(routeValues.MediaId);

        return Ok(media);
    }

    [HttpGet("/[controller]/OffsetPaged")]
    public async Task<ActionResult<OffsetPagedResponse<MediaResponse>>> GetOffsetPaged(
        [FromQuery] OffsetPagingRequest paging)
    {
        var result = await _mediaService.GetOffSetPagedAsync(paging.Adapt<PagingQuery>());

        var response = result.Adapt<OffsetPagedResponse<MediaResponse>>();

        return Ok(response);
    }
}