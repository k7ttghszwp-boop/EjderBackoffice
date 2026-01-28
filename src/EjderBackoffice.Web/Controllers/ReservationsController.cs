using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

using Ejder.Application.Products.Services;
using Ejder.Application.Reservations.Dtos;
using Ejder.Application.Reservations.Services;
using Ejder.Domain.Reservations;

namespace EjderBackoffice.Web.Controllers;

[Authorize]
public class ReservationsController : Controller
{
    private readonly IReservationService _reservations;
    private readonly IProductService _products;

    public ReservationsController(IReservationService reservations, IProductService products)
    {
        _reservations = reservations;
        _products = products;
    }

    // LIST + FILTER
    public IActionResult Index(string? q = null, ReservationStatus? status = null)
    {
        ViewBag.Q = q;
        ViewBag.Status = status;

        var list = _reservations.GetAll();

        if (!string.IsNullOrWhiteSpace(q))
        {
            q = q.Trim();
            list = list.Where(x =>
                (x.Pnr ?? "").Contains(q, StringComparison.OrdinalIgnoreCase) ||
                (x.CustomerName ?? "").Contains(q, StringComparison.OrdinalIgnoreCase) ||
                (x.TourName ?? "").Contains(q, StringComparison.OrdinalIgnoreCase)
            );
        }

        if (status.HasValue)
            list = list.Where(x => x.Status == status.Value);

        return View(list);
    }

    // DETAILS
    public IActionResult Details(int id)
    {
        var item = _reservations.GetById(id);
        if (item is null) return NotFound();
        return View(item);
    }

    // STATUS UPDATE
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult UpdateStatus(int id, ReservationStatus status)
    {
        _reservations.UpdateStatus(id, status);
        TempData["Success"] = "Durum başarıyla güncellendi ✅";
        return RedirectToAction(nameof(Details), new { id });
    }

    // CREATE (GET)
    public IActionResult Create(int? productId = null)
    {
        LoadToursToViewBag();

        if (productId.HasValue)
        {
            var tour = _products.GetAll().FirstOrDefault(x => x.Id == productId.Value);
            if (tour != null)
            {
                var dto = new ReservationCreateDto
                {
                    ProductId = tour.Id,
                    TourName = tour.Name,
                    UnitPrice = tour.Price,
                    TourDate = DateTime.Today.AddDays(7),
                    PersonCount = 1
                };
                return View(dto);
            }
        }

        return View(new ReservationCreateDto
        {
            TourDate = DateTime.Today.AddDays(7),
            PersonCount = 1
        });
    }

    // CREATE (POST)
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(ReservationCreateDto dto, int tourId)
    {
        var tour = _products.GetAll().FirstOrDefault(x => x.Id == tourId);
        if (tour is null)
            ModelState.AddModelError(nameof(tourId), "Lütfen bir tur seçin.");

        if (!ModelState.IsValid)
        {
            LoadToursToViewBag();
            return View(dto);
        }

        // DTO’yu domain entity’ye çevir
        var reservation = new Reservation
        {
            ProductId = tour!.Id,
            TourName = tour.Name,
            TourDate = dto.TourDate,
            PersonCount = dto.PersonCount,
            CustomerName = dto.CustomerName,
            Phone = dto.Phone,
            UnitPrice = tour.Price,
            AmountTry = tour.Price * (dto.PersonCount ?? 1),
            Status = ReservationStatus.Pending
        };

        _reservations.Create(reservation);

        TempData["Success"] = "Rezervasyon eklendi ✅";
        return RedirectToAction(nameof(Index));
    }

    private void LoadToursToViewBag()
    {
        ViewBag.Tours = _products.GetAll()
            .Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString()
            })
            .ToList();
    }
}
