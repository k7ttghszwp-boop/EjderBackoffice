using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Ejder.Core.Repositories;
using Ejder.Core.Models;

namespace EjderBackoffice.Web.Controllers;

[Authorize]
public class ReservationsController : Controller
{
    // =====================================================
    // 📌 LİSTE + FİLTRE
    // =====================================================
    public IActionResult Index(string? q = null, ReservationStatus? status = null)
    {
        ViewBag.Q = q;
        ViewBag.Status = status;

        var list = ReservationRepository.GetAll();

        if (!string.IsNullOrWhiteSpace(q))
        {
            q = q.Trim();
            list = list.Where(x =>
                (x.Pnr ?? "").Contains(q, StringComparison.OrdinalIgnoreCase) ||
                x.CustomerName.Contains(q, StringComparison.OrdinalIgnoreCase) ||
                x.TourName.Contains(q, StringComparison.OrdinalIgnoreCase)
            );
        }

        if (status.HasValue)
        {
            list = list.Where(x => x.Status == status.Value);
        }

        return View(list);
    }

    // =====================================================
    // 📌 DETAY
    // =====================================================
    public IActionResult Details(int id)
    {
        var item = ReservationRepository.GetById(id);
        if (item is null)
            return NotFound();

        return View(item);
    }

    // =====================================================
    // 📌 DURUM GÜNCELLE (ONAY → PNR)
    // =====================================================
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult UpdateStatus(int id, ReservationStatus status)
    {
        ReservationRepository.UpdateStatus(id, status);

        TempData["Success"] = "Durum başarıyla güncellendi ✅";
        return RedirectToAction(nameof(Details), new { id });
    }

    // =====================================================
    // 📌 MANUEL REZERVASYON (BACKOFFICE)
    // =====================================================

    // GET
    public IActionResult Create()
    {
        LoadToursToViewBag();

        var model = new Reservation
        {
            TourDate = DateTime.Today.AddDays(7),
            PersonCount = 1
        };

        return View(model);
    }

    // POST
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Reservation model, int tourId)
    {
        var tour = ProductRepository.GetAll()
            .FirstOrDefault(x => x.Id == tourId);

        if (tour is null)
            ModelState.AddModelError(nameof(tourId), "Lütfen bir tur seçin.");

        if (!ModelState.IsValid)
        {
            LoadToursToViewBag();
            return View(model);
        }

        // 🔹 Tur bilgileri
        model.ProductId = tour!.Id;
        model.TourName = tour.Name;

        // 🔹 Fiyat hesaplama
        model.UnitPrice = tour.Price;
        model.AmountTry = model.UnitPrice * (model.PersonCount ?? 1);

        ReservationRepository.Add(model);

        TempData["Success"] = "Rezervasyon eklendi ✅";
        return RedirectToAction(nameof(Index));
    }

    // =====================================================
    // 🔹 HELPERS
    // =====================================================
    private void LoadToursToViewBag()
    {
        ViewBag.Tours = ProductRepository.GetAll()
            .Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString()
            })
            .ToList();
    }
}
