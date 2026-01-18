using Microsoft.AspNetCore.Mvc;
using Ejder.Core.Repositories;
using Ejder.Core.Models;

namespace Ejder.Web.Public.Controllers;

public class ProductsController : Controller
{
    // 📌 Tur vitrini
    public IActionResult Index()
    {
        var products = ProductRepository.GetAll();
        return View(products);
    }

    // 📌 Tur detay (SEO URL)
    [Route("products/{slug}")]
    public IActionResult Details(string slug)
    {
        var product = ProductRepository.GetBySlug(slug);
        if (product == null)
            return NotFound();

        // 🔽 Gün gün program
        var program = TourProgramRepository.GetByProduct(product.Id);

        // 🔽 PDF doküman
        var doc = TourDocumentRepository.GetByProduct(product.Id);

        ViewBag.Program = program;
        ViewBag.Document = doc;

        return View(product);
    }
}
