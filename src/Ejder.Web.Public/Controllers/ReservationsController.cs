using Microsoft.AspNetCore.Mvc;

using Ejder.Application.Products.Services;
using Ejder.Application.Reservations.Services;
using Ejder.Domain.Reservations;

namespace Ejder.Web.Public.Controllers;

public class ReservationsController : Controller
{
    private readonly IProductService _products;
    private readonly IReservationService _reservations;

    public ReservationsController(IProductService products, IReservationService reservations)
    {
        _products = products;
        _reservations = reservations;
    }

    // /Reservations/Create?id=5
    [HttpGet]
    public async Task<IActionResult> Create(int id)
    {
        var product = await _products.GetByIdAsync(id);
        if (product is null)
            return NotFound();

        // product.Price zaten decimal ise direkt al
        var unitPrice = product.Price;

        var model = new Reservation
        {
            ProductId = product.Id,
            TourName = product.Name,
            PersonCount = 1,
            UnitPrice = unitPrice,
            AmountTry = unitPrice * 1
            // Status set etmiyoruz
        };

        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Reservation model)
    {
        var product = await _products.GetByIdAsync(model.ProductId);
        if (product is null)
            ModelState.AddModelError(nameof(model.ProductId), "Tur bulunamadı.");

        if (!ModelState.IsValid)
            return View(model);

        // Server-side garanti
        model.TourName = product!.Name;

        // UnitPrice / AmountTry hesap
        model.UnitPrice = product.Price;

        var count = model.PersonCount ?? 1;
        if (count <= 0) count = 1;
        model.PersonCount = count;

        model.AmountTry = model.UnitPrice * count;

        await _reservations.CreateAsync(model);

        TempData["Success"] = "Rezervasyon talebiniz alındı ✅";
        return RedirectToAction("Index", "Products");
    }
}

