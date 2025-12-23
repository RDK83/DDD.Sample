using API.Host.Controllers.Abstract;
using API.Models.Request.Merchants;
using API.Models.Response.Merchants;
using API.Models.RouteValues.Merchants;
using Catalogue.Application.Merchants;
using Catalogue.Application.Merchants.Commands;
using Catalogue.Application.Merchants.Queries;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Host.Controllers;

[Authorize]
[ApiController]
[Route("[controller]/{merchantId}")]
public class MerchantsController : ControllerBaseExtended<MerchantsController, MerchantRouteValues, MerchantResponse>,
    IGetByIdController<MerchantRouteValues, MerchantResponse>
{
    private readonly IMerchantService _merchantService;

    public MerchantsController(IMerchantService merchantService)
    {
        _merchantService = merchantService;
    }

    [HttpGet]
    public async Task<ActionResult<MerchantResponse>> GetById([FromRoute] MerchantRouteValues routeValues)
    {
        var query = new GetMerchantByIdQuery(routeValues.MerchantId);

        var merchantDto = await _merchantService.GetByIdAsync(query);

        var response = merchantDto.Adapt<MerchantResponse>();

        return Ok(response);
    }

    [HttpPost("/[Controller]")]
    public async Task<ActionResult<MerchantResponse>> Create([FromBody] CreateMerchantRequest request)
    {
        var command = request.Adapt<CreateMerchantCommand>();

        var dto = await _merchantService.CreateAsync(command);

        var response = dto.Adapt<MerchantResponse>();

        var routeValues = new MerchantRouteValues { MerchantId = dto.Id };

        return CreatedAt(routeValues, response);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromRoute] MerchantRouteValues routeValues, UpdateMerchantRequest request)
    {
        var command = request.Adapt<UpdateMerchantCommand>() with
        {
            MerchantId = routeValues.MerchantId
        };

        await _merchantService.UpdateAsync(command);

        return NoContent();
    }

    [HttpDelete]
    public async Task<IActionResult> Delete([FromRoute] MerchantRouteValues routeValues)
    {
        var command = new DeleteMerchantCommand(routeValues.MerchantId);

        await _merchantService.DeleteAsync(command);

        return NoContent();
    }
}