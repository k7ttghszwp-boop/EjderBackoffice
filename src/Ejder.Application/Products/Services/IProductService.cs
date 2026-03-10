using Ejder.Application.Products.Dtos;
using Ejder.Domain.Products;

namespace Ejder.Application.Products.Services;

public interface IProductService
{
    Task<IEnumerable<Product>> GetAllAsync();
    Task<Product?> GetByIdAsync(int id);
    Task<Product> CreateAsync(ProductCreateDto dto);
}
