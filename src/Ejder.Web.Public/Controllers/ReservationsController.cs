using Microsoft.AspNetCore.Mvc;
using Ejder.Core.Models;
using Ejder.Core.Repositories;

namespace Ejder.Web.Public.Controllers;

public class ReservationsController : Controller
{
    public IActionResult Create(int productId)
    {
        var product = ProductRepository.GetAll()
            .FirstOrDefault(x => x.Id == productId);

        if (product == null)
            return NotFound();

        var model = new Reservation
        {
            ProductId = product.Id,
            TourName = product.Name,
            UnitPrice = product.Price,
            PersonCount = 1,
            TourDate = DateTime.Today.AddDays(7)
        };

        ViewBag.Product = product;
        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Reservation model)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.Product = ProductRepository.GetAll()
                .FirstOrDefault(x => x.Id == model.ProductId);
            return View(model);
        }

        model.AmountTry = model.UnitPrice * model.PersonCount;
        model.Status = ReservationStatus.Yeni;

        ReservationRepository.Add(model);

        return RedirectToAction("Success");
    }

    public IActionResult Success()
    {
        return View();
    }
}
