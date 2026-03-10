namespace Ejder.Domain.Tours;
using Ejder.Domain;

public class TourDocument : BaseEntity
{
    public int ProductId { get; set; }
    public string FileName { get; set; } = "";
    public string FilePath { get; set; } = "";
}
