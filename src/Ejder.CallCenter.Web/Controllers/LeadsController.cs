using Microsoft.AspNetCore.Mvc;
using Ejder.Core.Repositories;
using Ejder.Core.Models;

namespace Ejder.CallCenter.Web.Controllers;

public class LeadsController : Controller
{
    public IActionResult Index()
    {
        // Şimdilik Yeni + Onaylanmamış rezervasyonlar
        var leads = ReservationRepository.GetAll()
            .Where(x => x.Status == ReservationStatus.Yeni)
            .OrderByDescending(x => x.CreatedAt);

        return View(leads);
    }
}
