using Microsoft.AspNetCore.Mvc;
using Ejder.Core.Repositories;
using Ejder.Core.Models;

namespace EjderBackoffice.Web.Controllers;

public class ToursController : Controller
{
    public IActionResult Index()
    {
        var tours = ProductRepository.GetAll();
        return View(tours);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Create(Product model)
    {
        if (!ModelState.IsValid)
            return View(model);

        ProductRepository.Add(model);
        return RedirectToAction("Index");
    }
}
