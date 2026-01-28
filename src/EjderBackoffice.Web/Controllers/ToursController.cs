using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Ejder.Application.Products.Services;
using Ejder.Application.Products.Dtos;

namespace EjderBackoffice.Web.Controllers;

[Authorize]
public class ToursController : Controller
{
    private readonly IProductService _productService;

    public ToursController(IProductService productService)
    {
        _productService = productService;
    }

    public IActionResult Index()
    {
        var tours = _productService.GetAll();
        return View(tours);
    }

    public IActionResult Create()
    {
        return View(new ProductCreateDto());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(ProductCreateDto model)
    {
        if (!ModelState.IsValid)
            return View(model);

        _productService.Create(model);
        TempData["Success"] = "Tur başarıyla eklendi ✅";

        return RedirectToAction(nameof(Index));
    }
}
