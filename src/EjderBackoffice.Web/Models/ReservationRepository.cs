using System.Globalization;

namespace EjderBackoffice.Web.Models;

public static class ReservationRepository
{
    private static readonly List<Reservation> Data = new()
    {
        new Reservation { Id=1, Pnr="GHMJZM", CustomerName="Ahmet Yılmaz", TourName="Küba Turu",     TourDate=DateTime.Today.AddDays(7),  AmountTry=58450,  Status=ReservationStatus.Onaylandi },
        new Reservation { Id=2, Pnr="GISAXH", CustomerName="Elif Demir",   TourName="Japonya-Kore",  TourDate=DateTime.Today.AddDays(14), AmountTry=112900, Status=ReservationStatus.Yeni },
        new Reservation { Id=3, Pnr="FCTWQJ", CustomerName="Mehmet Kaya",  TourName="Latin Amerika", TourDate=DateTime.Today.AddDays(21), AmountTry=98750,  Status=ReservationStatus.Yeni },
        new Reservation { Id=4, Pnr="AB12CD", CustomerName="Selin Ak",     TourName="Brezilya",      TourDate=DateTime.Today.AddDays(10), AmountTry=76500,  Status=ReservationStatus.Iptal },
    };

public static IEnumerable<Reservation> GetAll(string? q = null, ReservationStatus? status = null)
{
    var list = Data.AsEnumerable();

    if (!string.IsNullOrWhiteSpace(q))
    {
        q = q.Trim();

        list = list.Where(x =>
            ContainsInsensitive(x.Pnr, q) ||
            ContainsInsensitive(x.CustomerName, q) ||
            ContainsInsensitive(x.TourName, q) ||
            x.Status.ToString().Contains(q, StringComparison.OrdinalIgnoreCase)
        );
    }

    if (status.HasValue)
    {
        list = list.Where(x => x.Status == status.Value);
    }

    return list.OrderByDescending(x => x.Id);
}
    public static Reservation? GetById(int id) => Data.FirstOrDefault(x => x.Id == id);

    public static void UpdateStatus(int id, ReservationStatus status)
    {
        var item = Data.FirstOrDefault(x => x.Id == id);
        if (item != null)
            item.Status = status;
    }

    private static bool ContainsInsensitive(string source, string value)
        => source?.IndexOf(value, StringComparison.OrdinalIgnoreCase) >= 0;


public static Reservation? GetByPnr(string pnr)
{
    if (string.IsNullOrWhiteSpace(pnr)) return null;
    pnr = pnr.Trim();
    return Data.FirstOrDefault(x => x.Pnr.Equals(pnr, StringComparison.OrdinalIgnoreCase));
}
public static void Add(Reservation model)
{
    model.Id = Data.Any() ? Data.Max(x => x.Id) + 1 : 1;
    model.Status = ReservationStatus.Yeni;

    if (string.IsNullOrWhiteSpace(model.Pnr))
        model.Pnr = Guid.NewGuid().ToString("N")[..6].ToUpper();

    Data.Add(model);
}


}
