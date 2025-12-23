using API.Host.Controllers.Abstract;
using API.Models.Response.ProductTypes;
using API.Models.RouteValues.ProductTypes;
using Catalogue.Application.ProductTypes;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Host.Controllers;

[Authorize]
[ApiController]
[Route("[controller]/{productTypeId}")]
public class ProductTypesController :
    ControllerBaseExtended<ProductTypesController, ProductTypeRouteValues, ProductTypeResponse>,
    IGetByIdController<ProductTypeRouteValues, ProductTypeResponse>
{
    private readonly IProductTypeService _productTypeService;

    public ProductTypesController(IProductTypeService productTypeService)
    {
        _productTypeService = productTypeService;
    }

    [HttpGet]
    public async Task<ActionResult<ProductTypeResponse>> GetById([FromRoute] ProductTypeRouteValues routeValues)
    {
        var dto = await _productTypeService.GetByIdAsync(routeValues.ProductTypeId);

        var response = dto.Adapt<ProductTypeResponse>();

        return Ok(response);
    }
}