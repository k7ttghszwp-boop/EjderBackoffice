using Ejder.Domain.HR;

namespace Ejder.Domain.Repositories;

public interface IAttendanceRepository : IRepository<Attendance>
{
    Task<Attendance?> GetTodayByEmployeeIdAsync(int employeeId);
    Task<IEnumerable<Attendance>> GetTodayAllAsync();
}
