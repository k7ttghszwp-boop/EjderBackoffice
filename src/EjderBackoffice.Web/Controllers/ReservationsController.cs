using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using EjderBackoffice.Web.Models;

namespace EjderBackoffice.Web.Controllers;

[Authorize]
public class ReservationsController : Controller
{
    public IActionResult Index(string? q = null, ReservationStatus? status = null)
    {
        ViewBag.Q = q;
        ViewBag.Status = status;
        var list = ReservationRepository.GetAll(q, status);
        return View(list);
    }

    public IActionResult Details(int id)
    {
        var item = ReservationRepository.GetById(id);
        if (item is null) return NotFound();
        return View(item);
    }

    [HttpPost]
    public IActionResult UpdateStatus(int id, ReservationStatus status)
    {
        ReservationRepository.UpdateStatus(id, status);
        TempData["Success"] = "Durum başarıyla güncellendi ✅";
        return RedirectToAction("Details", new { id });
    }

    // ✅ GET: /Reservations/Create
    public IActionResult Create()
    {
        ViewBag.Tours = TourRepository.GetAll()
            .Select(x => new SelectListItem(x.Name, x.Id.ToString()))
            .ToList();

        // Default tarih vs.
        var model = new Reservation { TourDate = DateTime.Today.AddDays(7) };
        return View(model);
    }

    // ✅ POST: /Reservations/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Reservation model, int tourId)
    {
        var tour = TourRepository.GetById(tourId);
        if (tour is null)
            ModelState.AddModelError("", "Lütfen bir tur seçin.");

        if (!ModelState.IsValid)
        {
            ViewBag.Tours = TourRepository.GetAll()
                .Select(x => new SelectListItem(x.Name, x.Id.ToString()))
                .ToList();
            return View(model);
        }

        // dropdown seçimine göre tur adını set ediyoruz
        model.TourName = tour!.Name;

        ReservationRepository.Add(model);
        TempData["Success"] = "Rezervasyon eklendi ✅";
        return RedirectToAction("Index");
    }
}
