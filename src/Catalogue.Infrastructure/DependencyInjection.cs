using System.Reflection;
using Catalogue.Infrastructure.ConfigModels;
using Catalogue.Infrastructure.Persistence;
using Catalogue.Infrastructure.Persistence.Interceptors;
using Catalogue.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Shared.Application.UnitOfWork;
using SharedKernel.Repositories;

namespace Catalogue.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services,
        ConnectionStringOptions connectionStringOptions)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(connectionStringOptions.Application));

        services.AddRepositories();
        services.AddUnitOfWork();
        services.AddInterceptors();
        services.AddMediatorService();

        return services;
    }

    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        var assembly = Assembly.GetAssembly(typeof(WarehouseRepository));
        if (assembly != null)
        {
            services.Scan(s => s
                .FromAssemblies(assembly)
                .AddClasses(c => c.Where(t => t.Name.EndsWith("Repository") && !t.IsGenericTypeDefinition))
                .AsImplementedInterfaces()
                .WithScopedLifetime());
        }

        services.AddScoped(typeof(ILookupReadRepository<,>),
            typeof(LookupRepository<,>));

        return services;
    }

    public static IServiceCollection AddMediatorService(this IServiceCollection services)
    {
        services.AddMediator(options => { options.ServiceLifetime = ServiceLifetime.Scoped; });
        return services;
    }

    public static IServiceCollection AddUnitOfWork(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        return services;
    }

    public static IServiceCollection AddInterceptors(this IServiceCollection services)
    {
        services.AddScoped<UpdateTimestampsInterceptor>();
        return services;
    }
}