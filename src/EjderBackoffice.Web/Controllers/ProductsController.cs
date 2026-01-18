using Microsoft.AspNetCore.Mvc;
using EjderBackoffice.Web.Models;

namespace EjderBackoffice.Web.Controllers.Public;

public class ProductsController : Controller
{
    public IActionResult Index()
    {
        var list = TourRepository.GetAll(onlyActive: true);
        return View(list);
    }

    public IActionResult Detail(int id)
    {
        var item = TourRepository.GetById(id);
        if (item == null) return NotFound();
        return View(item);
    }
}
