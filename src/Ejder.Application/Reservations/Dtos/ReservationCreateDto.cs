using System.ComponentModel.DataAnnotations;

namespace Ejder.Application.Reservations.Dtos;

public class ReservationCreateDto
{
    [Required]
    public int ProductId { get; set; }

    [Required]
    public string TourName { get; set; } = "";

    [Required]
    public decimal UnitPrice { get; set; }

    [Required]
    public DateTime? TourDate { get; set; }

    [Required]
    [Range(1, 999)]
    public int? PersonCount { get; set; }

    [Required]
    public string CustomerName { get; set; } = "";

    [Required]
    public string Phone { get; set; } = "";
}
