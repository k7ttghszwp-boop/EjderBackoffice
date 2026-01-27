using Ejder.Domain.Tours;

namespace Ejder.Application.Tours.Services;

public interface ITourDocumentService
{
    TourDocument? GetByProduct(int productId);
    void Save(TourDocument doc);
}
