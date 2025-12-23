using Catalogue.Domain.Products.ValueObjects;
using SharedKernel.Repositories;

namespace Catalogue.Domain.Products.Repositories;

public interface IProductReadRepository : IReadRepository<Product>, IKeysetPagedRepository<Product, ProductCode>;