using Microsoft.AspNetCore.Mvc;

namespace Ejder.Marketing.Web.Controllers;

public class HomeController : Controller
{
    public IActionResult Index() => View();

    public IActionResult Packages() => View();

    public IActionResult Contact() => View();

    public IActionResult Demo() => View();
}
