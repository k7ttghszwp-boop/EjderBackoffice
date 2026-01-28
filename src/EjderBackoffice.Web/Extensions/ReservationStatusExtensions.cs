using Ejder.Domain.Reservations;

namespace EjderBackoffice.Web.Extensions;

public static class ReservationStatusExtensions
{
    public static string ToBadgeClass(this ReservationStatus status) => status switch
    {
        ReservationStatus.Pending => "badge bg-warning text-dark",
        ReservationStatus.Approved => "badge bg-success",
        ReservationStatus.Rejected => "badge bg-danger",
        ReservationStatus.Cancelled => "badge bg-secondary",
        _ => "badge bg-light text-dark"
    };

    public static string ToDisplay(this ReservationStatus status) => status switch
    {
        ReservationStatus.Pending => "Bekliyor",
        ReservationStatus.Approved => "Onaylandı",
        ReservationStatus.Rejected => "Reddedildi",
        ReservationStatus.Cancelled => "İptal",
        _ => status.ToString()
    };
}
