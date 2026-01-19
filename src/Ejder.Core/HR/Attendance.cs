namespace Ejder.Core.HR;

public class Attendance
{
    public int Id { get; set; }
    public int EmployeeId { get; set; }

    public DateTime Date { get; set; } = DateTime.Today;

    public DateTime? CheckIn { get; set; }
    public DateTime? CheckOut { get; set; }
}
