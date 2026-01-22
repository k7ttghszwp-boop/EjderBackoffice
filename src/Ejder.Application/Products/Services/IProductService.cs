using Ejder.Application.Products.Dtos;
using Ejder.Domain.Products;

namespace Ejder.Application.Products.Services;

public interface IProductService
{
    List<Product> GetAll();
    Product? GetById(int id);
    Product Create(ProductCreateDto dto);
}
