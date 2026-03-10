using Ejder.Domain.Tours;

namespace Ejder.Application.Tours.Services;

public interface ITourProgramService
{
    Task<IEnumerable<TourProgramDay>> GetByProductAsync(int productId);
    Task AddDayAsync(TourProgramDay day);
    Task AddAsync(TourProgramDay day);
}
