using Ejder.Core.Models;

namespace Ejder.Core.Repositories;

public static class ReservationRepository
{
    private static readonly List<Reservation> Data = new();

    public static IEnumerable<Reservation> GetAll()
        => Data.OrderByDescending(x => x.CreatedAt);

    public static Reservation? GetById(int id)
        => Data.FirstOrDefault(x => x.Id == id);

    public static void Add(Reservation model)
    {
        model.Id = Data.Any() ? Data.Max(x => x.Id) + 1 : 1;
        model.CreatedAt = DateTime.Now;
        model.Status = ReservationStatus.Yeni;

        Data.Add(model);
    }

    public static void UpdateStatus(int id, ReservationStatus status)
    {
        var item = Data.FirstOrDefault(x => x.Id == id);
        if (item == null) return;

        item.Status = status;

        if (status == ReservationStatus.Onaylandi && string.IsNullOrWhiteSpace(item.Pnr))
        {
            item.Pnr = Guid.NewGuid().ToString("N")[..6].ToUpper();
        }
    }
}
