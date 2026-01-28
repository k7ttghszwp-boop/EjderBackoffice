using Ejder.Domain.Tours;

namespace Ejder.Application.Tours.Services;

public interface ITourProgramService
{
    List<TourProgramDay> GetByProduct(int productId);
    void AddDay(TourProgramDay day);
    void Add(TourProgramDay day);
}
