namespace Ejder.Domain.Reservations;
using Ejder.Domain;

public class Reservation : BaseEntity
{
    public int ProductId { get; set; }
    public string TourName { get; set; } = "";

    public DateTime? TourDate { get; set; }
    public int? PersonCount { get; set; }

    public string CustomerName { get; set; } = "";
    public string Phone { get; set; } = "";

    public decimal UnitPrice { get; set; }
    public decimal AmountTry { get; set; }

    public string? Pnr { get; set; }

    public ReservationStatus Status { get; set; } = ReservationStatus.Pending;
}
