namespace Ejder.Domain.HR;
using Ejder.Domain;

public class Employee : BaseEntity
{
    public string FullName { get; set; } = "";
    public string Email { get; set; } = "";

    // Şifre hashlenmiş olarak saklanır
    public string PasswordHash { get; set; } = "";

    public int DepartmentId { get; set; }
    public int? ReportsToEmployeeId { get; set; }

    public EmployeeRole Role { get; set; }

    public bool IsActive { get; set; } = true;
}
