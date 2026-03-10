using Ejder.Domain.Products;
using Ejder.Domain.Repositories;
using Ejder.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Ejder.Infrastructure.Repositories;

public class ProductRepository : Repository<Product>, IProductRepository
{
    public ProductRepository(AppDbContext db) : base(db)
    {
    }

    public async Task<Product?> GetBySlugAsync(string slug)
    {
        return await _dbSet.FirstOrDefaultAsync(p => p.Slug == slug && !p.IsDeleted);
    }
}
