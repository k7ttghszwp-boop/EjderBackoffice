using Ejder.Domain.Reservations;

namespace Ejder.Application.Reservations.Services;

public interface IReservationService
{
    Task<IEnumerable<Reservation>> GetAllAsync();
    Task<Reservation?> GetByIdAsync(int id);
    Task<Reservation> CreateAsync(Reservation reservation);
    Task UpdateStatusAsync(int id, ReservationStatus status);
}
