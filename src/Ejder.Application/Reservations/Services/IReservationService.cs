using Ejder.Domain.Reservations;

namespace Ejder.Application.Reservations.Services;

public interface IReservationService
{
    IEnumerable<Reservation> GetAll();
    Reservation? GetById(int id);
    Reservation Create(Reservation reservation);
    void UpdateStatus(int id, ReservationStatus status);
}
