using Ejder.Core.Models;

namespace Ejder.Core.Repositories;

public static class TourDocumentRepository
{
    private static readonly List<TourDocument> Data = new();

    public static TourDocument? GetByProduct(int productId)
        => Data.FirstOrDefault(x => x.ProductId == productId);

    public static void Save(TourDocument doc)
    {
        var existing = GetByProduct(doc.ProductId);
        if (existing != null)
        {
            existing.FileName = doc.FileName;
            existing.FilePath = doc.FilePath;
        }
        else
        {
            doc.Id = Data.Any() ? Data.Max(x => x.Id) + 1 : 1;
            Data.Add(doc);
        }
    }
}
