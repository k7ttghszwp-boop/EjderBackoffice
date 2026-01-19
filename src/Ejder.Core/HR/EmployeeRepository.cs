namespace Ejder.Core.HR;

public static class EmployeeRepository
{
    private static readonly List<Employee> Data = new()
    {
        new Employee {
            Id = 1,
            FullName = "Resul Ersürer",
            Email = "resul@ejder.com",
            DepartmentId = 1,
            Role = EmployeeRole.Manager
        },
        new Employee {
            Id = 2,
            FullName = "Serdar Duvarbaşi",
            Email = "serdar@ejder.com",
            DepartmentId = 1,
            Role = EmployeeRole.Supervisor,
            ReportsToEmployeeId = 1
        },
        new Employee {
            Id = 3,
            FullName = "Ahmet Yılmaz",
            Email = "ahmet@ejder.com",
            DepartmentId = 1,
            Role = EmployeeRole.Staff,
            ReportsToEmployeeId = 2
        }
    };

    public static IEnumerable<Employee> GetAll() => Data;

    public static Employee? GetById(int id)
        => Data.FirstOrDefault(x => x.Id == id);

    public static IEnumerable<Employee> GetByManager(int managerId)
        => Data.Where(x => x.ReportsToEmployeeId == managerId);
}
