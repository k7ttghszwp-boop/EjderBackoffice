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

    // =====================================================
    // BASIC QUERIES
    // =====================================================
    public static IEnumerable<Employee> GetAll() => Data;

    public static Employee? GetById(int id)
        => Data.FirstOrDefault(x => x.Id == id);

    public static Employee? GetByEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            return null;

        return Data.FirstOrDefault(x =>
            x.Email != null &&
            x.Email.Equals(email.Trim(), StringComparison.OrdinalIgnoreCase)
        );
    }

    public static IEnumerable<Employee> GetDirectReports(int managerId)
        => Data.Where(x => x.ReportsToEmployeeId == managerId);

    // =====================================================
    // LOGIN (MVP - password yoksa email ile)
    // =====================================================
    public static Employee? Login(string email, string? password = null)
    {
        // Şimdilik sadece email ile doğruluyoruz.
        // İleride Employee modeline Password ekleyince burada kontrol yapacağız.
        return GetByEmail(email);
    }

    // =====================================================
    // ORG TREE (Manager -> Supervisor -> Staff ... hepsi)
    // =====================================================
    public static HashSet<int> GetTeamTreeIds(int managerId)
    {
        var result = new HashSet<int>();
        var queue = new Queue<int>();
        queue.Enqueue(managerId);

        while (queue.Count > 0)
        {
            var currentId = queue.Dequeue();

            foreach (var emp in Data.Where(x => x.ReportsToEmployeeId == currentId))
            {
                if (result.Add(emp.Id))
                {
                    // Bu kişinin de altında ekip olabilir
                    queue.Enqueue(emp.Id);
                }
            }
        }

        // result: manager’ın altındaki herkes (manager hariç)
        return result;
    }

    public static IEnumerable<Employee> GetTeamTree(int managerId)
    {
        var ids = GetTeamTreeIds(managerId);
        return Data.Where(x => ids.Contains(x.Id));
    }
}
