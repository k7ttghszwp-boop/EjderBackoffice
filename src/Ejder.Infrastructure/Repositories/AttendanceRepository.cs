using Ejder.Domain.HR;
using Ejder.Domain.Repositories;
using Ejder.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Ejder.Infrastructure.Repositories;

public class AttendanceRepository : Repository<Attendance>, IAttendanceRepository
{
    public AttendanceRepository(AppDbContext db) : base(db)
    {
    }

    public async Task<Attendance?> GetTodayByEmployeeIdAsync(int employeeId)
    {
        var today = DateTime.Today;
        return await _dbSet.FirstOrDefaultAsync(x => x.EmployeeId == employeeId && x.Date == today);
    }

    public async Task<IEnumerable<Attendance>> GetTodayAllAsync()
    {
        var today = DateTime.Today;
        return await _dbSet.Where(x => x.Date == today).ToListAsync();
    }
}
