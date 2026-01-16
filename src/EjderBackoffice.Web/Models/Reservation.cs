using System.ComponentModel.DataAnnotations;

namespace EjderBackoffice.Web.Models;

public class Reservation
{
    public int Id { get; set; }

    [Display(Name = "PNR")]
    public string? Pnr { get; set; }

    [Required(ErrorMessage = "Müşteri adı zorunludur")]
    [Display(Name = "Müşteri Adı")]
    public string CustomerName { get; set; } = "";

    [Required(ErrorMessage = "Tur adı zorunludur")]
    [Display(Name = "Tur")]
    public string TourName { get; set; } = "";

    [Required]
    [Display(Name = "Tur Tarihi")]
    public DateTime TourDate { get; set; }

    [Range(1, 1_000_000_000, ErrorMessage = "Tutar 0'dan büyük olmalıdır")]
    [Display(Name = "Tutar (₺)")]
    public decimal AmountTry { get; set; }

    public ReservationStatus Status { get; set; } = ReservationStatus.Yeni;
}

public enum ReservationStatus
{
    Yeni,
    Onaylandi,
    Iptal
}
