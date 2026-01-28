using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Authorize] // giriş şart
public class DashboardController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
[Authorize(Roles = "Admin")]
public class SettingsController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
