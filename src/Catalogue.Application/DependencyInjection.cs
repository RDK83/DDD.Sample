using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Catalogue.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        var assembly = Assembly.GetAssembly(typeof(Warehouses.WarehouseService));
        if (assembly != null)
        {
            services.Scan(s => s
                .FromAssemblies(assembly)
                .AddClasses(c => c.Where(t => t.Name.EndsWith("Service") || t.Name.EndsWith("Factory") || t.Name.EndsWith("Provider")))
                .AsImplementedInterfaces()
                .WithScopedLifetime());
        }

        return services;
    }
}