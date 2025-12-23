using Catalogue.Infrastructure.Exceptions;
using Shared.Domain.Lookups;
using SharedKernel.Repositories;

namespace Catalogue.Infrastructure.Persistence.Repositories;

public class LookupRepository<TLookup, TId> : ILookupRepository<TLookup, TId> where TLookup : LookupObject<TId>
{
    protected readonly ApplicationDbContext ApplicationDbContext;

    public LookupRepository(ApplicationDbContext applicationDbContext)
    {
        ApplicationDbContext = applicationDbContext;
    }

    public async Task<TLookup> GetByIdAsync(TId id)
    {
        var lookup = await ApplicationDbContext.Set<TLookup>().FindAsync(id);

        if (lookup is null)
            throw new LookupNotFoundException(typeof(TLookup).Name, id?.ToString());

        return lookup;
    }

    public async Task VerifyExistsAsync(TId id)
    {
        var lookup = await ApplicationDbContext.Set<TLookup>().FindAsync(id);

        if (lookup is null)
            throw new LookupNotFoundException(typeof(TLookup).Name, id?.ToString());
    }

    public void Add(TLookup lookup)
    {
        ApplicationDbContext.Set<TLookup>().Add(lookup);
    }

    public void Remove(TLookup lookup)
    {
        ApplicationDbContext.Remove(lookup);
    }
}