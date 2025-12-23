using SharedKernel.Repositories;

namespace Catalogue.Domain.Products.Repositories;

public interface IProductRepository : IRepository<Product>, IProductReadRepository;