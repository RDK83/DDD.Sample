using API.Host;
using API.Host.ConfigModels.DocumentationOptions;
using API.Host.Extensions;
using Catalogue.Application;
using Catalogue.Application.Shared.Logging;
using Catalogue.Infrastructure;
using Catalogue.Infrastructure.ConfigModels;
using Catalogue.Infrastructure.Persistence;
using Catalogue.Infrastructure.Persistence.Configurations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

var documentationOptions = builder.Configuration.GetSection<DocumentationOptions>();
var connectionStringOptions = builder.Configuration.GetSection<ConnectionStringOptions>();

builder.Services.AddControllers();

builder.Services.AddOpenApi(documentationOptions);
builder.Services.AddHttpContextAccessor();
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructure(connectionStringOptions);

var app = builder.Build();

if (documentationOptions.Enabled)
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseExceptionHandling();
app.UseHttpsRedirection();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers().WithMetadata(new AllowAnonymousAttribute());

var loggerFactory = app.Services.GetRequiredService<ILoggerFactory>();
LogMethodAttribute.Configure(loggerFactory);

var retries = 10;

while (retries-- > 0)
{
    try
    {
        using var scope = app.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        db.Database.Migrate();
        SeedData.Initialize(db);
        break;
    }
    catch
    {
        if (retries == 0) throw;
        Thread.Sleep(2000);
    }
}

app.Run();