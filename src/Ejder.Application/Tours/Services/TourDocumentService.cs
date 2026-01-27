using Ejder.Domain.Tours;

namespace Ejder.Application.Tours.Services;

public class TourDocumentService : ITourDocumentService
{
    private static readonly List<TourDocument> _docs = new();

    public TourDocument? GetByProduct(int productId)
        => _docs.FirstOrDefault(x => x.ProductId == productId);

    public void Save(TourDocument doc)
    {
        // aynı productId için overwrite
        var existing = _docs.FirstOrDefault(x => x.ProductId == doc.ProductId);
        if (existing != null) _docs.Remove(existing);

        doc.Id = _docs.Count == 0 ? 1 : _docs.Max(x => x.Id) + 1;
        _docs.Add(doc);
    }
}
