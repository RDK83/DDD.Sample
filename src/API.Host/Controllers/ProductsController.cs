using API.Host.Controllers.Abstract;
using API.Models.Pagination;
using API.Models.Request.Products;
using API.Models.Response.Products;
using API.Models.RouteValues.Products;
using Catalogue.Application.Products;
using Catalogue.Application.Products.Commands;
using Catalogue.Application.Products.Commands.Medias;
using Catalogue.Application.Products.Commands.Offers;
using Catalogue.Application.Products.Queries;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Application.Pagination;

namespace API.Host.Controllers;

[Authorize]
[ApiController]
[Route("[controller]/{productCode}")]
public class ProductsController : ControllerBaseExtended<ProductsController, ProductRouteValues, ProductResponse>,
    IGetByIdController<ProductRouteValues, ProductResponse>
{
    private readonly IProductService _productService;

    public ProductsController(IProductService productService)
    {
        _productService = productService;
    }

    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpGet]
    public async Task<ActionResult<ProductResponse>> GetById([FromRoute] ProductRouteValues routeValues)
    {
        var query = new GetProductByIdQuery(routeValues.ProductCode);

        var productDto = await _productService.GetByIdAsync(query);

        var productResponse = productDto.Adapt<ProductResponse>();

        return Ok(productResponse);
    }

    [HttpGet("/[controller]/OffsetPaged")]
    public async Task<ActionResult<OffsetPagedResponse<ProductResponse>>> GetOffsetPaged(
        [FromQuery] ProductFilter filter, [FromQuery] OffsetPagingRequest paging)
    {
        var result = await _productService.GetOffSetPagedAsync(filter, paging.Adapt<PagingQuery>());

        var response = result.Adapt<OffsetPagedResponse<ProductResponse>>();

        return Ok(response);
    }

    [HttpGet("/[controller]/KeysetPaged")]
    public async Task<ActionResult<KeysetPagedResponse<ProductResponse, string>>> GetKeysetPaged(
        [FromQuery] ProductKeysetPagingRequest paging)
    {
        var result =
            await _productService.GetKeysetPagedAsync(paging.Adapt<PagingQuery>());

        var response = result.Adapt<KeysetPagedResponse<ProductResponse, string>>();

        return Ok(response);
    }

    [ProducesResponseType(StatusCodes.Status201Created)]
    [HttpPost("/[controller]")]
    public async Task<ActionResult<ProductResponse>> Create([FromBody] CreateProductRequest request)
    {
        var command = request.Adapt<CreateProductCommand>();

        var dto = await _productService.CreateAsync(command);

        var routeValues = new ProductRouteValues
            { ProductCode = dto.ProductCode };

        var response = dto.Adapt<ProductResponse>();

        return CreatedAt(routeValues, response);
    }

    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpPut]
    public async Task<IActionResult> Update([FromRoute] ProductRouteValues routeValues,
        [FromBody] UpdateProductRequest request)
    {
        var command = request.Adapt<UpdateProductCommand>() with
        {
            ProductCode = routeValues.ProductCode
        };

        await _productService.UpdateAsync(command);

        return NoContent();
    }

    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpDelete]
    public async Task<IActionResult> Delete([FromRoute] ProductRouteValues routeValues)
    {
        var command = new DeleteProductCommand(routeValues.ProductCode);

        await _productService.DeleteAsync(command);

        return NoContent();
    }

    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpPost("Offers")]
    public async Task<IActionResult> CreateOffer([FromRoute] ProductRouteValues routeValues,
        [FromBody] CreateOfferRequest request)
    {
        var command = request.Adapt<CreateOfferCommand>() with
        {
            ProductCode = routeValues.ProductCode
        };

        await _productService.CreateProductOfferAsync(command);

        return Created();
    }

    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpPut("Offers/{offerId}")]
    public async Task<IActionResult> UpdateOffer([FromRoute] ProductOfferRouteValues routeValues,
        [FromBody] UpdateOfferRequest request)
    {
        var command = request.Adapt<UpdateOfferCommand>() with
        {
            ProductCode = routeValues.ProductCode,
            Id = routeValues.OfferId
        };

        await _productService.UpdateProductOfferAsync(command);

        return NoContent();
    }

    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpDelete("Offers/{offerId}")]
    public async Task<IActionResult> DeleteOffer([FromRoute] ProductOfferRouteValues routeValues)
    {
        var command =
            new DeleteOfferCommand(routeValues.OfferId, routeValues.ProductCode);

        await _productService.DeleteProductOfferAsync(command);

        return NoContent();
    }

    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpPost("Medias")]
    public async Task<IActionResult> CreateMedia([FromRoute] ProductRouteValues routeValues, int mediaId)
    {
        var command = new CreateProductMediaCommand(routeValues.ProductCode, mediaId);

        await _productService.CreateProductMediaAsync(command);

        return Created();
    }
}