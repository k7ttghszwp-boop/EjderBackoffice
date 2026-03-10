using Microsoft.AspNetCore.Mvc;

namespace Ejder.Web.Public.Controllers;

public class CategoryController : Controller
{
    [Route("{lang}/category/{id}")]
    [Route("category/{id}")]
    public IActionResult ToursByCategory(Guid id, string lang = "tr")
    {
        // Tours/Index metoduna yönlendiriyoruz, o zaten kategori filtresini destekliyor.
        return RedirectToAction("Index", "Tours", new { lang = lang, categoryId = id });
    }
}
