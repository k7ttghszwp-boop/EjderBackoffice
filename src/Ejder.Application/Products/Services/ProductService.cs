using Ejder.Application.Products.Dtos;
using Ejder.Domain.Products;

namespace Ejder.Application.Products.Services;

public class ProductService : IProductService
{
    private static readonly List<Product> _products = new();

    public List<Product> GetAll()
        => _products;

    public Product? GetById(int id)
        => _products.FirstOrDefault(x => x.Id == id);

    public Product Create(ProductCreateDto dto)
    {
        var product = new Product
        {
            Id = _products.Count == 0 ? 1 : _products.Max(x => x.Id) + 1,
            Name = dto.Name,
            Days = dto.Days,
            Price = dto.Price,
            IsActive = true
        };

        _products.Add(product);
        return product;
    }
}
