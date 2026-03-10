using Ejder.Domain.Tours;
using Ejder.Domain.Repositories;

namespace Ejder.Application.Tours.Services;

public class TourProgramService : ITourProgramService
{
    private readonly IRepository<TourProgramDay> _repo;

    public TourProgramService(IRepository<TourProgramDay> repo)
    {
        _repo = repo;
    }

    public async Task<IEnumerable<TourProgramDay>> GetByProductAsync(int productId)
    {
        return await _repo.FindAsync(x => x.ProductId == productId);
    }

    public async Task AddDayAsync(TourProgramDay day)
    {
        await _repo.AddAsync(day);
        await _repo.SaveChangesAsync();
    }

    public async Task AddAsync(TourProgramDay day)
    {
        // basic guard
        if (day.DayNumber <= 0) day.DayNumber = 1;

        await _repo.AddAsync(day);
        await _repo.SaveChangesAsync();
    }
}

