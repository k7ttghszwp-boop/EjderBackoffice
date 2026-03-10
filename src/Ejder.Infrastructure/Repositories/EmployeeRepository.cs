using Ejder.Domain.HR;
using Ejder.Domain.Repositories;
using Ejder.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using BC = BCrypt.Net.BCrypt;

namespace Ejder.Infrastructure.Repositories;

public class EmployeeRepository : Repository<Employee>, IEmployeeRepository
{
    public EmployeeRepository(AppDbContext db) : base(db)
    {
    }

    public async Task<Employee?> GetByEmailAsync(string email)
    {
        return await _dbSet.FirstOrDefaultAsync(e => e.Email == email && !e.IsDeleted);
    }

    public async Task<Employee?> LoginAsync(string email, string password)
    {
        var employee = await GetByEmailAsync(email);
        if (employee == null || !employee.IsActive) return null;

        // MVP: Eğer hashli değilse (başlangıçta öyle olabilir) basit kontrol veya BCrypt
        try 
        {
            if (BC.Verify(password, employee.PasswordHash))
                return employee;
        }
        catch 
        {
            // Hash değilse düz metin kontrolü (Geliştirme aşaması için geçici)
            if (employee.PasswordHash == password)
                return employee;
        }

        return null;
    }
}
