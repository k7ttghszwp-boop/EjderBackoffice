using Microsoft.AspNetCore.Mvc;
using Ejder.Application.Reservations.Services;
using Ejder.Domain.Reservations;

namespace Ejder.CallCenter.Web.Controllers;

public class LeadsController : Controller
{
    private readonly IReservationService _reservationService;

    public LeadsController(IReservationService reservationService)
    {
        _reservationService = reservationService;
    }

    public async Task<IActionResult> Index()
    {
        // Şimdilik Yeni + Onaylanmamış rezervasyonlar
        var all = await _reservationService.GetAllAsync();
        var leads = all.Where(x => x.Status == ReservationStatus.Pending);

        return View(leads);
    }
}

