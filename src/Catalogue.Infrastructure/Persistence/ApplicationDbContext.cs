using System.Reflection;
using Catalogue.Infrastructure.Persistence.Interceptors;
using Microsoft.EntityFrameworkCore;
using Shared.Domain.Events;

namespace Catalogue.Infrastructure.Persistence;

public class ApplicationDbContext : DbContext
{
    private readonly UpdateTimestampsInterceptor _timestampsInterceptor;

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options,
        UpdateTimestampsInterceptor timestampsInterceptor) : base(options)
    {
        _timestampsInterceptor = timestampsInterceptor;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Ignore<BaseDomainEvent>();
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.AddInterceptors(_timestampsInterceptor);
    }
}