using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Ejder.Core.HR;

namespace Ejder.Employee.Web.Controllers;

public class PortalController : Controller
{
    // 🔹 MVP demo personel
    private static Ejder.Core.HR.Employee DemoUser = new()
    {
        Id = 1,
        FullName = "Ejder Personel",
        Email = "personel@ejder.com",
        Password = "1234"
    };

    // =====================================================
    // LOGIN
    // =====================================================
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Login(string email, string password)
    {
        if (email == DemoUser.Email && password == DemoUser.Password)
        {
            HttpContext.Session.SetInt32("EmployeeId", DemoUser.Id);
            HttpContext.Session.SetString("EmployeeName", DemoUser.FullName);

            return RedirectToAction(nameof(Dashboard));
        }

        ViewBag.Error = "Giriş bilgileri hatalı";
        return View();
    }

    // =====================================================
    // DASHBOARD
    // =====================================================
    public IActionResult Dashboard()
    {
        var id = GetEmployeeId();
        if (id == null)
            return RedirectToAction(nameof(Login));

        ViewBag.EmployeeName = HttpContext.Session.GetString("EmployeeName");
        ViewBag.Today = AttendanceRepository.GetToday(id.Value);

        return View();
    }

    // =====================================================
    // GİRİŞ / ÇIKIŞ
    // =====================================================
    [HttpPost]
    public IActionResult CheckIn()
    {
        var id = GetEmployeeId();
        if (id != null)
            AttendanceRepository.CheckIn(id.Value);

        return RedirectToAction(nameof(Dashboard));
    }

    [HttpPost]
    public IActionResult CheckOut()
    {
        var id = GetEmployeeId();
        if (id != null)
            AttendanceRepository.CheckOut(id.Value);

        return RedirectToAction(nameof(Dashboard));
    }

    // =====================================================
    // LOGOUT
    // =====================================================
    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction(nameof(Login));
    }

    // =====================================================
    // HELPER
    // =====================================================
    private int? GetEmployeeId()
    {
        return HttpContext.Session.GetInt32("EmployeeId");
    }
}
