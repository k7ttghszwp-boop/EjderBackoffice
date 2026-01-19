namespace Ejder.Web.Public.Models;

public class TourDate
{
    public int Id { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }

    public decimal PriceTry { get; set; }          // Bu tarihe özel fiyat
    public int Capacity { get; set; }              // Toplam kontenjan
    public int Reserved { get; set; }              // Dolu koltuk

    public int Available => Math.Max(0, Capacity - Reserved);

    public string RangeText => $"{StartDate:dd MMM} - {EndDate:dd MMM yyyy}";
}
