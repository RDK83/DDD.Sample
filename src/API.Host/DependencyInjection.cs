using API.Host.ConfigModels.DocumentationOptions;
using API.Host.Extensions;
using Scalar.AspNetCore;

namespace API.Host;

public static class DependencyInjection
{
    public static IServiceCollection AddOpenApi(this IServiceCollection services,
        DocumentationOptions documentationOptions)
    {
        if (documentationOptions.Enabled)
        {
            services.AddOpenApi(options =>
            {
                options.AddScalarTransformers();
                options.AddAllParametersDescribedInCamelCase();
            });
        }

        return services;
    }
}