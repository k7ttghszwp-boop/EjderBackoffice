namespace Ejder.Core.Models;

public class TourProgramDay
{
    public int Id { get; set; }
    public int ProductId { get; set; }   // hangi tur
    public int DayNumber { get; set; }   // 1,2,3...
    public string Title { get; set; } = "";
    public string Description { get; set; } = "";
}
