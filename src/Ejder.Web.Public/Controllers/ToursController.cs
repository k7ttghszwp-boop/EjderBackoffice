using Microsoft.AspNetCore.Mvc;
using Ejder.Web.Public.Models;

namespace Ejder.Web.Public.Controllers;

public class ToursController : Controller
{
    public IActionResult Index()
    {
        var tours = TourRepository.GetAll();
        return View(tours);
    }

    public IActionResult Detail(int id)
    {
        var tour = TourRepository.GetById(id);
        if (tour == null) return NotFound();

        return View(tour);
    }

    [HttpPost]
    public IActionResult RequestTour(int id, int tourDateId, string fullName, string phone, string email, int pax, string note)
    {
        var tour = TourRepository.GetById(id);
        if (tour == null) return NotFound();

        var date = tour.Dates.FirstOrDefault(x => x.Id == tourDateId);
        if (date == null)
        {
            TempData["Ok"] = null;
            TempData["Err"] = "Lütfen geçerli bir tur tarihi seçin.";
            return RedirectToAction(nameof(Detail), new { id });
        }

        // MVP: şimdilik sadece mesaj
        TempData["Ok"] = $"Talebiniz alındı. Seçilen tarih: {date.RangeText}. Ekibimiz sizi arayacak.";

        return RedirectToAction(nameof(Detail), new { id });
    }
}
