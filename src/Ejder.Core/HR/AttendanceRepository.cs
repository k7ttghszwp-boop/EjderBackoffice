namespace Ejder.Core.HR;

public static class AttendanceRepository
{
    private static readonly List<Attendance> Data = new();

    public static Attendance? GetToday(int employeeId)
    {
        var today = DateTime.Today;

        return Data.FirstOrDefault(x =>
            x.EmployeeId == employeeId &&
            x.Date == today
        );
    }

    public static void CheckIn(int employeeId)
    {
        if (GetToday(employeeId) != null)
            return;

        Data.Add(new Attendance
        {
            Id = Data.Any() ? Data.Max(x => x.Id) + 1 : 1,
            EmployeeId = employeeId,
            Date = DateTime.Today,          // ✅ KRİTİK
            CheckIn = DateTime.Now
        });
    }

    public static void CheckOut(int employeeId)
    {
        var record = GetToday(employeeId);

        if (record != null && record.CheckOut == null)
        {
            record.CheckOut = DateTime.Now;
        }
    }
    public static List<Attendance> GetTodayAll()
    {
        var today = DateTime.Today;
        return Data
            .Where(x => x.Date == today)
            .ToList();
    }

}
