using Ejder.Application.Products.Dtos;
using Ejder.Domain.Products;
using Ejder.Domain.Repositories;

namespace Ejder.Application.Products.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _repo;

    public ProductService(IProductRepository repo)
    {
        _repo = repo;
    }

    public async Task<IEnumerable<Product>> GetAllAsync()
        => await _repo.GetAllAsync();

    public async Task<Product?> GetByIdAsync(int id)
        => await _repo.GetByIdAsync(id);

    public async Task<Product> CreateAsync(ProductCreateDto dto)
    {
        var product = new Product
        {
            Name = dto.Name,
            Days = dto.Days,
            Price = dto.Price,
            IsActive = true
        };

        await _repo.AddAsync(product);
        await _repo.SaveChangesAsync();
        return product;
    }
}

