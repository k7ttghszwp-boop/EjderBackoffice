using Ejder.Core.Models;

namespace Ejder.Core.Repositories;

public static class ProductRepository
{
    private static readonly List<Product> Data = new()
    {
        new Product
        {
            Id = 1,
            Name = "Küba Turu",
            DurationDays = 7,
            Price = 79900,
            Slug = "kuba-turu"
        },
        new Product
        {
            Id = 2,
            Name = "Japonya & Kore",
            DurationDays = 10,
            Price = 129900,
            Slug = "japonya-kore"
        }
    };

    public static IEnumerable<Product> GetAll() => Data;

    // ✅ BUNU EKLE
    public static Product? GetBySlug(string slug)
    {
        if (string.IsNullOrWhiteSpace(slug))
            return null;

        return Data.FirstOrDefault(x =>
            x.Slug.Equals(slug, StringComparison.OrdinalIgnoreCase)
        );
    }
    public static void Add(Product product)
    {
        product.Id = Data.Any() ? Data.Max(x => x.Id) + 1 : 1;

        if (string.IsNullOrWhiteSpace(product.Slug))
            product.Slug = GenerateSlug(product.Name);

        Data.Add(product);
    }

    public static void Update(Product product)
    {
        var existing = Data.FirstOrDefault(x => x.Id == product.Id);
        if (existing == null) return;

        existing.Name = product.Name;
        existing.DurationDays = product.DurationDays;
        existing.Price = product.Price;
        existing.Slug = product.Slug;
    }

    private static string GenerateSlug(string text)
    {
        return text
            .ToLowerInvariant()
            .Replace(" ", "-")
            .Replace("&", "and")
            .Replace("ı", "i")
            .Replace("ö", "o")
            .Replace("ü", "u")
            .Replace("ç", "c")
            .Replace("ş", "s")
            .Replace("ğ", "g");
    }
}
