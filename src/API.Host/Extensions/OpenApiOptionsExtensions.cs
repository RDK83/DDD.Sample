using System.Reflection;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.OpenApi;

namespace API.Host.Extensions;

public static class OpenApiOptionsExtensions
{
    public static OpenApiOptions AddObsoleteSupport(this OpenApiOptions options)
    {
        return options.AddOperationTransformer((operation, context, ct) =>
        {
            var ad = context.Description.ActionDescriptor;

            // 1) Obsolete on endpoint metadata
            var isObsolete = ad.EndpointMetadata?.OfType<ObsoleteAttribute>().Any() == true;

            // 2) Obsolete on MVC action or controller
            if (!isObsolete && ad is ControllerActionDescriptor cad)
            {
                isObsolete =
                    cad.MethodInfo.GetCustomAttribute<ObsoleteAttribute>() != null ||
                    cad.ControllerTypeInfo.GetCustomAttribute<ObsoleteAttribute>() != null;
            }

            if (isObsolete)
                operation.Deprecated = true;

            return Task.CompletedTask;
        });
    }

    public static OpenApiOptions AddAllParametersDescribedInCamelCase(this OpenApiOptions options)
    {
        options.AddOperationTransformer((operation, context, _) =>
        {
            if (operation.Parameters is null)
                return Task.CompletedTask;

            var updatedParameters = new List<IOpenApiParameter>();

            foreach (var parameter in operation.Parameters)
            {
                if (parameter.In != ParameterLocation.Path)
                    continue;

                var newParameter = new OpenApiParameter
                {
                    Name = char.ToUpperInvariant(parameter.Name[0])
                           + parameter.Name[1..],
                    In = parameter.In,
                    Schema = parameter.Schema,
                    Style = parameter.Style
                };

                updatedParameters.Add(newParameter);
            }

            operation.Parameters.Clear();
            operation.Parameters = updatedParameters;
            return Task.CompletedTask;
        });

        return options;
    }
}