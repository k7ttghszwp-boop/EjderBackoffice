using System.ComponentModel.DataAnnotations;

namespace Ejder.Core.Models;

public class Reservation
{
    public int Id { get; set; }

    public string? Pnr { get; set; }

    [Required]
    public string CustomerName { get; set; } = "";

    [Required]
    public string Phone { get; set; } = "";

    public int? ProductId { get; set; }

    public string TourName { get; set; } = "";

    public DateTime? TourDate { get; set; }

    public int? PersonCount { get; set; }

    public decimal? UnitPrice { get; set; }

    public decimal? AmountTry { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public ReservationStatus Status { get; set; } = ReservationStatus.Yeni;
}

public enum ReservationStatus
{
    Yeni,
    Onaylandi,
    Iptal
}
