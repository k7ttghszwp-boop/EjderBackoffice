namespace Ejder.Domain.Tours;
using Ejder.Domain;

public class TourProgramDay : BaseEntity
{
    public int ProductId { get; set; }
    public int DayNumber { get; set; }
    public string Title { get; set; } = "";
    public string Description { get; set; } = "";
}
