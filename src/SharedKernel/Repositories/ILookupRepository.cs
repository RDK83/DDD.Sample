namespace SharedKernel.Repositories;

public interface ILookupRepository<TLookup, in TId> : ILookupReadRepository<TLookup, TId>
{
    void Add(TLookup lookup);
    void Remove(TLookup entity);
}