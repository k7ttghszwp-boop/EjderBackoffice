namespace Ejder.Core.HR;

public class Employee
{
    public int Id { get; set; }

    public string FullName { get; set; } = "";
    public string Email { get; set; } = "";

    // 🔴 MVP ONLY – ileride kaldırılacak
    public string Password { get; set; } = "";

    public int DepartmentId { get; set; }
    public int? ReportsToEmployeeId { get; set; }

    public EmployeeRole Role { get; set; }

    public bool IsActive { get; set; } = true;
}
