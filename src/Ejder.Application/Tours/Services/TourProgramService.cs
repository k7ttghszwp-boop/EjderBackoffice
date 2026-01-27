using Ejder.Domain.Tours;

namespace Ejder.Application.Tours.Services;

public class TourProgramService : ITourProgramService
{
    // basit in-memory store
    private static readonly List<TourProgramDay> _days = new();

    public List<TourProgramDay> GetByProduct(int productId)
        => _days.Where(x => x.ProductId == productId)
                .OrderBy(x => x.DayNumber)
                .ToList();

    public void AddDay(TourProgramDay day)
    {
        day.Id = _days.Count == 0 ? 1 : _days.Max(x => x.Id) + 1;
        _days.Add(day);
    }
}
