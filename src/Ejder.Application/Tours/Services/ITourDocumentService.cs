using Ejder.Domain.Tours;

namespace Ejder.Application.Tours.Services;

public interface ITourDocumentService
{
    Task<TourDocument?> GetByProductAsync(int productId);
    Task SaveAsync(TourDocument doc);
}
