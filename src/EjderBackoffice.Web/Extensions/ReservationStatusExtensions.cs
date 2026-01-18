using Ejder.Core.Models;

namespace EjderBackoffice.Web.Extensions;

public static class ReservationStatusExtensions
{
    public static string ToText(this ReservationStatus s) => s switch
    {
        ReservationStatus.Onaylandi => "Onaylandı",
        ReservationStatus.Iptal => "İptal",
        _ => "Yeni"
    };

    public static string ToBadgeClass(this ReservationStatus s) => s switch
    {
        ReservationStatus.Onaylandi => "badge badge-green",
        ReservationStatus.Iptal => "badge badge-red",
        _ => "badge badge-yellow"
    };
}
