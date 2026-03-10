using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ejder.Domain.Entities;

namespace Ejder.Domain.Interfaces
{
    public interface ITourRepository
    {
        Task<IEnumerable<Tour>> GetAllActiveAsync();
        Task<IEnumerable<Tour>> GetAllActiveByCategoryAsync(Guid categoryId);
        Task<Tour?> GetByIdAsync(Guid id);
        Task AddAsync(Tour tour);
        Task UpdateAsync(Tour tour);
        Task DeleteAsync(Guid id);
        Task<(IEnumerable<Tour> Items, int TotalCount)> GetPagedAsync(int page, int pageSize, Guid? categoryId);
    }
}
