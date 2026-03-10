using Ejder.Domain.Reservations;
using Ejder.Domain.Repositories;

namespace Ejder.Application.Reservations.Services;

public class ReservationService : IReservationService
{
    private readonly IReservationRepository _repo;

    public ReservationService(IReservationRepository repo)
    {
        _repo = repo;
    }

    public async Task<IEnumerable<Reservation>> GetAllAsync() 
        => await _repo.GetAllAsync();

    public async Task<Reservation?> GetByIdAsync(int id) 
        => await _repo.GetByIdAsync(id);

    public async Task<Reservation> CreateAsync(Reservation reservation)
    {
        if (reservation.AmountTry <= 0 && reservation.UnitPrice > 0 && (reservation.PersonCount ?? 0) > 0)
        {
            reservation.AmountTry = reservation.UnitPrice * (reservation.PersonCount ?? 0);
        }

        await _repo.AddAsync(reservation);
        await _repo.SaveChangesAsync();
        return reservation;
    }

    public async Task UpdateStatusAsync(int id, ReservationStatus status)
    {
        var item = await _repo.GetByIdAsync(id);
        if (item == null) return;

        item.Status = status;

        if (status == ReservationStatus.Approved && string.IsNullOrWhiteSpace(item.Pnr))
        {
            item.Pnr = Guid.NewGuid().ToString("N")[..6].ToUpper();
        }

        _repo.Update(item);
        await _repo.SaveChangesAsync();
    }
}

