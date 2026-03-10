using Ejder.Domain.Products;

namespace Ejder.Domain.Repositories;

public interface IProductRepository : IRepository<Product>
{
    Task<Product?> GetBySlugAsync(string slug);
}
