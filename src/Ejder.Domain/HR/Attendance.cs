namespace Ejder.Domain.HR;
using Ejder.Domain;

public class Attendance : BaseEntity
{
    public int EmployeeId { get; set; }

    public DateTime Date { get; set; } = DateTime.Today;

    public DateTime? CheckIn { get; set; }
    public DateTime? CheckOut { get; set; }
}
