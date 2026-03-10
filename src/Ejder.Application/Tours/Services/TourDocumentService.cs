using Ejder.Domain.Tours;
using Ejder.Domain.Repositories;

namespace Ejder.Application.Tours.Services;

public class TourDocumentService : ITourDocumentService
{
    private readonly IRepository<TourDocument> _repo;

    public TourDocumentService(IRepository<TourDocument> repo)
    {
        _repo = repo;
    }

    public async Task<TourDocument?> GetByProductAsync(int productId)
    {
        var docs = await _repo.FindAsync(x => x.ProductId == productId);
        return docs.FirstOrDefault();
    }

    public async Task SaveAsync(TourDocument doc)
    {
        // aynı productId için overwrite
        var docs = await _repo.FindAsync(x => x.ProductId == doc.ProductId);
        var existing = docs.FirstOrDefault();
        
        if (existing != null) 
        {
            _repo.Remove(existing);
        }

        await _repo.AddAsync(doc);
        await _repo.SaveChangesAsync();
    }
}

