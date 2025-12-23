using System.Data;
using Microsoft.EntityFrameworkCore;

namespace Catalogue.Infrastructure.Persistence.Repositories.Abstract;

public abstract class DapperApplicationReadRepository(ApplicationDbContext context)
{
    protected readonly IDbConnection DbConnection = context.Database.GetDbConnection();
}