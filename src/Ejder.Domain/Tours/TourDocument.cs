namespace Ejder.Domain.Tours;

public class TourDocument
{
    public int Id { get; set; }          // şimdilik
    public int ProductId { get; set; }
    public string FileName { get; set; } = "";
    public string FilePath { get; set; } = "";
}
