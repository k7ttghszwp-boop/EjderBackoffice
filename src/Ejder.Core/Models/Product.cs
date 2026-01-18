namespace Ejder.Core.Models;

public class Product
{
    public int Id { get; set; }

    public string Name { get; set; } = "";

    public int DurationDays { get; set; }   // ⬅️ TUR SÜRESİ

    public decimal Price { get; set; }

    public string Slug { get; set; } = "";
}
