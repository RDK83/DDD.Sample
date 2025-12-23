using System.Text.Json;
using Catalogue.Infrastructure.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Shared.Domain.Exceptions;
using Shared.Domain.Exceptions.Abstract;

namespace API.Host.Middleware;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            int statusCode;
            string title;

            switch (ex)
            {
                case EntityNotFoundException enf:
                    _logger.LogInformation("Entity not found: {EntityName}", enf.EntityName);
                    statusCode = StatusCodes.Status404NotFound;
                    title = "Entity not found";
                    break;

                case LookupNotFoundException lnf:
                    _logger.LogInformation("Lookup not found: {LookupName} with ID {Id}", lnf.LookupName, lnf.Identifier);
                    statusCode = StatusCodes.Status404NotFound;
                    title = "Lookup not found";
                    break;

                case ChildEntityNotFoundException cenf:
                    _logger.LogInformation("Child entity not found: {ChildName} of parent {ParentId}", cenf.ChildEntityName, cenf.ParentIdentifier);
                    statusCode = StatusCodes.Status400BadRequest;
                    title = "Child entity not found";
                    break;

                case ChildEntityAlreadyExistsException ceae:
                    _logger.LogInformation("Child entity already exists: {ChildName} for parent {ParentId}", ceae.ChildEntityName, ceae.ParentIdentifier);
                    statusCode = StatusCodes.Status409Conflict;
                    title = "Entity already exists";
                    break;

                case DomainValidationException dve:
                    _logger.LogWarning("Domain validation failed: {Message}", dve.Message);
                    statusCode = StatusCodes.Status400BadRequest;
                    title = "Domain validation failed";
                    break;

                case DomainException de:
                    _logger.LogWarning("Domain error: {Message}", de.Message);
                    statusCode = StatusCodes.Status400BadRequest;
                    title = "Domain error";
                    break;

                case InvalidSpecificationException ise:
                    _logger.LogWarning("Invalid specification: {Message}", ise.Message);
                    statusCode = StatusCodes.Status400BadRequest;
                    title = "Invalid Specification";
                    break;

                case DuplicateKeyViolationException dkve:
                    _logger.LogInformation("Duplicate key violation: {Message}", dkve.Message);
                    statusCode = StatusCodes.Status409Conflict;
                    title = "Duplicate key";
                    break;

                case UniqueConstraintViolationException ucve:
                    _logger.LogInformation("Unique constraint violation: {Message}", ucve.Message);
                    statusCode = StatusCodes.Status409Conflict;
                    title = "Unique constraint violation";
                    break;

                case ForeignKeyViolationException fkve:
                    _logger.LogInformation("Foreign key violation: {Message}", fkve.Message);
                    statusCode = StatusCodes.Status422UnprocessableEntity;
                    title = "Foreign key violation";
                    break;

                default:
                    // Unexpected exceptions
                    _logger.LogError(ex, "Unhandled exception caught by middleware");
                    statusCode = StatusCodes.Status500InternalServerError;
                    title = "Internal Server Error";
                    break;
            }

            var problem = new ProblemDetails
            {
                Status = statusCode,
                Title = title,
                Detail = ex.Message,
                Instance = context.Request.Path
            };

            context.Response.StatusCode = statusCode;
            context.Response.ContentType = "application/json";

            var options = new JsonSerializerOptions {PropertyNamingPolicy = JsonNamingPolicy.CamelCase};
            await context.Response.WriteAsync(JsonSerializer.Serialize(problem, options));
        }
    }
}