namespace Ejder.Core.Models;

public class Employee
{
    public int Id { get; set; }

    public string FullName { get; set; } = "";

    public string Email { get; set; } = "";

    // MVP için düz text, sonra hashlenir
    public string Password { get; set; } = "";

    public int? DepartmentId { get; set; }

    public int? ManagerId { get; set; }
}
