using Microsoft.AspNetCore.Mvc;
using Ejder.Core.Repositories;

namespace Ejder.Web.Public.Controllers;

[Route("turlar")]
public class ProductsController : Controller
{
    [HttpGet("")]
    public IActionResult Index()
    {
        var products = ProductRepository.GetAll();
        return View(products);
    }

    [HttpGet("{slug}")]
    public IActionResult Details(string slug)
    {
        var product = ProductRepository.GetBySlug(slug);
        if (product == null) return NotFound();
        return View(product);
    }
}

