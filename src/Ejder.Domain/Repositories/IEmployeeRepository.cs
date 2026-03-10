using Ejder.Domain.HR;

namespace Ejder.Domain.Repositories;

public interface IEmployeeRepository : IRepository<Employee>
{
    Task<Employee?> GetByEmailAsync(string email);
    Task<Employee?> LoginAsync(string email, string password);
}
