using API.Host.Controllers.Abstract;
using API.Models.Request.Warehouses;
using API.Models.Response.Warehouses;
using API.Models.RouteValues.Warehouses;
using Catalogue.Application.Warehouses;
using Catalogue.Application.Warehouses.Commands;
using Catalogue.Application.Warehouses.Responses;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Host.Controllers;

[Authorize]
[ApiController]
[Route("[controller]/{warehouseId}")]
public class WarehousesController :
    ControllerBaseExtended<WarehousesController, WarehouseRouteValues, WarehouseResponse>,
    IGetByIdController<WarehouseRouteValues, WarehouseResponse>
{
    private readonly IWarehouseService _warehouseService;

    public WarehousesController(IWarehouseService warehouseService, ILogger<WarehousesController> logger)
    {
        _warehouseService = warehouseService;
    }

    [HttpGet]
    public async Task<ActionResult<WarehouseResponse>> GetById([FromRoute] WarehouseRouteValues routeValues)
    {
        var warehouseDto = await _warehouseService.GetByIdAsync(routeValues.WarehouseId);

        var response = warehouseDto.Adapt<WarehouseDto>();

        return Ok(response);
    }

    [HttpPost("/[controller]")]
    public async Task<ActionResult<WarehouseResponse>> Create([FromBody] CreateWarehouseRequest request)
    {
        var warehouseCommand = request.Adapt<CreateWarehouseCommand>();

        var dto = await _warehouseService.CreateAsync(warehouseCommand);

        var routeValues = new WarehouseRouteValues { WarehouseId = dto.Id };

        var response = dto.Adapt<WarehouseResponse>();

        return CreatedAt(routeValues, response);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromRoute] WarehouseRouteValues routeValues,
        [FromBody] UpdateWarehouseRequest request)
    {
        var command = request.Adapt<UpdateWarehouseCommand>() with { WarehouseId = routeValues.WarehouseId };

        await _warehouseService.UpdateAsync(command);

        return NoContent();
    }

    [HttpDelete]
    public async Task<IActionResult> Remove([FromRoute] WarehouseRouteValues routeValues)
    {
        var command = new DeleteWarehouseCommand(routeValues.WarehouseId);

        await _warehouseService.DeleteAsync(command);

        return NoContent();
    }
}