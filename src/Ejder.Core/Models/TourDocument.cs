namespace Ejder.Core.Models;

public class TourDocument
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public string FileName { get; set; } = "";
    public string FilePath { get; set; } = "";
}
