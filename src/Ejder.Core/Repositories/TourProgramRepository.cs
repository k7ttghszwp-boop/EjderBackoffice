using Ejder.Core.Models;

namespace Ejder.Core.Repositories;

public static class TourProgramRepository
{
    private static readonly List<TourProgramDay> Data = new();

    public static IEnumerable<TourProgramDay> GetByProduct(int productId)
        => Data.Where(x => x.ProductId == productId).OrderBy(x => x.DayNumber);

    public static void Add(TourProgramDay day)
    {
        day.Id = Data.Any() ? Data.Max(x => x.Id) + 1 : 1;
        Data.Add(day);
    }
}
