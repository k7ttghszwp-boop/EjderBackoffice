namespace Ejder.Domain.Tours;

public class TourProgramDay
{
    public int Id { get; set; }          // şimdilik
    public int ProductId { get; set; }
    public int DayNumber { get; set; }
    public string Title { get; set; } = "";
    public string Description { get; set; } = "";
}
