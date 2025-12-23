namespace SharedKernel.Repositories;

public interface ILookupReadRepository<TLookup, in TId>
{
    Task<TLookup> GetByIdAsync(TId id);
    Task VerifyExistsAsync(TId id);
}