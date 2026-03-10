using Ejder.Domain.Repositories;
using Ejder.Domain.Reservations;
using Ejder.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Ejder.Infrastructure.Repositories;

public class ReservationRepository : Repository<Reservation>, IReservationRepository
{
    public ReservationRepository(AppDbContext db) : base(db)
    {
    }

    public override async Task<IEnumerable<Reservation>> GetAllAsync()
    {
        return await _db.Reservations
            .Where(x => !x.IsDeleted)
            .OrderByDescending(x => x.Id)
            .ToListAsync();
    }
}
