namespace Ejder.Domain.Products;

public class Product
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int Days { get; set; }

    public decimal Price { get; set; }

    public bool IsActive { get; set; } = true;
}
