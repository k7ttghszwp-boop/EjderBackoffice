namespace Ejder.Application.Products.Dtos;

public class ProductCreateDto
{
    public string Name { get; set; } = null!;
    public int Days { get; set; }
    public decimal Price { get; set; }
}
