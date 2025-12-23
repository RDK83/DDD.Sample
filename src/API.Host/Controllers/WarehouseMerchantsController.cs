using API.Host.Controllers.Abstract;
using API.Models.Request.WarehouseMerchants;
using API.Models.Response.WarehouseMerchants;
using API.Models.RouteValues.WarehouseMerchants;
using Catalogue.Application.WarehouseMerchants;
using Catalogue.Application.WarehouseMerchants.Commands;
using Catalogue.Application.WarehouseMerchants.Queries;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Host.Controllers;

[Authorize]
[ApiController]
[Route("[controller]/{warehouseMerchantId}")]
public class WarehouseMerchantsController(
    IWarehouseMerchantService warehouseMerchantService)
    : ControllerBaseExtended<WarehouseMerchantsController, WarehouseMerchantRouteValues, WarehouseMerchantResponse>,
        IGetByIdController<WarehouseMerchantRouteValues, WarehouseMerchantResponse>
{
    [HttpGet("/[Controller]")]
    public async Task<ActionResult<List<WarehouseMerchantResponse>>> Get()
    {
        var warehouseMerchants = await warehouseMerchantService.GetAllAsync();

        return warehouseMerchants.Adapt<List<WarehouseMerchantResponse>>();
    }

    [HttpGet]
    public async Task<ActionResult<WarehouseMerchantResponse>> GetById(
        [FromRoute] WarehouseMerchantRouteValues routeValues)
    {
        var query = new GetWarehouseMerchantByIdQuery(routeValues.WarehouseMerchantId);

        var dto = await warehouseMerchantService.GetByIdAsync(query);

        return dto.Adapt<WarehouseMerchantResponse>();
    }

    [HttpPost("/[Controller]")]
    public async Task<ActionResult<WarehouseMerchantResponse>> Create(CreateWarehouseMerchantRequest request)
    {
        var createCommand = request.Adapt<CreateWarehouseMerchantCommand>();
        var dto = await warehouseMerchantService.CreateAsync(createCommand);

        var routeValues = new WarehouseMerchantRouteValues { WarehouseMerchantId = dto.Id };

        var response = dto.Adapt<WarehouseMerchantResponse>();

        return CreatedAt(routeValues, response);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromRoute] WarehouseMerchantRouteValues routeValues,
        UpdateWarehouseMerchantRequest request)
    {
        var updateCommand = request.Adapt<UpdateWarehouseMerchantCommand>() with
        {
            Id = routeValues.WarehouseMerchantId
        };

        await warehouseMerchantService.UpdateAsync(updateCommand);

        return NoContent();
    }

    [HttpDelete]
    public async Task<ActionResult> Delete([FromRoute] WarehouseMerchantRouteValues routeValues)
    {
        var command = new DeleteWarehouseMerchantCommand(routeValues.WarehouseMerchantId);
        await warehouseMerchantService.DeleteAsync(command);
        return NoContent();
    }
}