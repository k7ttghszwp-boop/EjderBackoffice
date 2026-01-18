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
}
