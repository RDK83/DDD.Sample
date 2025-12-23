using API.Models.RouteValues;
using Microsoft.AspNetCore.Mvc;

namespace API.Host.Controllers.Abstract;

//The TController here is necessary to enforce the TController -> IGetByIdController relationship so we get compile time safety
public abstract class ControllerBaseExtended<TController, TRouteValues, TResponse>
    : ControllerBase
    where TController : ControllerBase, IGetByIdController<TRouteValues, TResponse>
    where TRouteValues : IRouteValues
{
    protected ActionResult<TResponse> CreatedAt(TRouteValues routeValues, TResponse response)
    {
        return CreatedAtAction(
            nameof(IGetByIdController<TRouteValues, TResponse>.GetById),
            routeValues,
            response
        );
    }
}