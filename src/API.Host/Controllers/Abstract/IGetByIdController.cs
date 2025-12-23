using API.Models.RouteValues;
using Microsoft.AspNetCore.Mvc;

namespace API.Host.Controllers.Abstract;

//The reason this is done as an interface and not rolled into the BaseControllerExtended is so that we don't have to override the method in the concrete controllers
public interface IGetByIdController<in TRouteValues, TResponse> where TRouteValues : IRouteValues
{
    Task<ActionResult<TResponse>> GetById(TRouteValues routeValues);
}