namespace Ejder.Web.Public.Models;

public class Tour
{
    public int Id { get; set; }
    public string Title { get; set; } = "";
    public string Country { get; set; } = "";
    public int Days { get; set; }
    public decimal PriceTry { get; set; }
    public string Summary { get; set; } = "";
    public string ImageUrl { get; set; } = "";

    public List<string> Highlights { get; set; } = new();
    public List<string> Included { get; set; } = new();
    public List<string> NotIncluded { get; set; } = new();
    public List<string> Program { get; set; } = new(); // Gün gün
    public List<TourDate> Dates { get; set; } = new();

}
