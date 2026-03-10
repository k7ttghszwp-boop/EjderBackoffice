using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ejder.Domain.Entities;
using Ejder.Domain.Interfaces;
using Ejder.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Ejder.Infrastructure.Repositories;

public class TourRepository : ITourRepository
{
    private readonly AppDbContext _context;

    public TourRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Tour>> GetAllActiveAsync()
    {
        return await _context.Tours
            .Include(t => t.Category)
            .Where(t => t.IsActive)
            .ToListAsync();
    }

    public async Task<IEnumerable<Tour>> GetAllActiveByCategoryAsync(Guid categoryId)
    {
        return await _context.Tours
            .Include(t => t.Category)
            .Where(t => t.IsActive && t.CategoryId == categoryId)
            .ToListAsync();
    }

    public async Task<Tour?> GetByIdAsync(Guid id)
    {
        return await _context.Tours
            .Include(t => t.Category)
            .FirstOrDefaultAsync(t => t.Id == id);
    }

    public async Task AddAsync(Tour tour)
    {
        await _context.Tours.AddAsync(tour);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Tour tour)
    {
        _context.Tours.Update(tour);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var tour = await _context.Tours.FindAsync(id);
        if (tour != null)
        {
            _context.Tours.Remove(tour);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<(IEnumerable<Tour> Items, int TotalCount)> GetPagedAsync(int page, int pageSize, Guid? categoryId)
    {
        var query = _context.Tours
            .Include(t => t.Category)
            .AsQueryable();

        if (categoryId.HasValue)
        {
            query = query.Where(t => t.CategoryId == categoryId.Value);
        }

        var totalCount = await query.CountAsync();
        
        var items = await query
            .OrderByDescending(t => t.CreatedAt)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return (items, totalCount);
    }
}
