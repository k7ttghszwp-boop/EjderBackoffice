namespace Ejder.Domain.Products;
using Ejder.Domain;

public class Product : BaseEntity
{
    public string Name { get; set; } = null!;

    public int Days { get; set; }

    public decimal Price { get; set; }

    public string Slug { get; set; } = string.Empty;

    public string? Summary { get; set; }
    public string? ImageUrl { get; set; }

    // Basit JSON or semicolon separated string or just lists (mapped as JSON in EF Core if needed)
    public List<string> Highlights { get; set; } = new();
    public List<string> Included { get; set; } = new();
    public List<string> NotIncluded { get; set; } = new();

    public bool IsActive { get; set; } = true;
}
