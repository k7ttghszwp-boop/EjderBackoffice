using Ejder.Domain.Reservations;

namespace Ejder.Application.Reservations.Services;

public class ReservationService : IReservationService
{
    // PR4: in-memory (sonra EF repo’ya geçilecek)
    private static readonly List<Reservation> _items = new();
    private static int _seq = 1;

    public IEnumerable<Reservation> GetAll() => _items.OrderByDescending(x => x.Id);

    public Reservation? GetById(int id) => _items.FirstOrDefault(x => x.Id == id);

    public Reservation Create(Reservation reservation)
    {
        reservation.Id = _seq++;
        if (reservation.AmountTry <= 0 && reservation.UnitPrice > 0 && (reservation.PersonCount ?? 0) > 0)
        {
            reservation.AmountTry = reservation.UnitPrice * (reservation.PersonCount ?? 0);
        }

        _items.Add(reservation);
        return reservation;
    }

    public void UpdateStatus(int id, ReservationStatus status)
    {
        var item = GetById(id);
        if (item == null) return;
        item.Status = status;
    }
}
