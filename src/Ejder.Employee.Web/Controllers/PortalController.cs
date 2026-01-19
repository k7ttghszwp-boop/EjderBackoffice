using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Ejder.Core.HR;

namespace Ejder.Employee.Web.Controllers;

public class PortalController : Controller
{
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
        // MVP: EmployeeRepository (şimdilik sadece email ile)
        var user = EmployeeRepository.Login(email, password);

        if (user != null)
        {
            HttpContext.Session.SetInt32("EmployeeId", user.Id);
            HttpContext.Session.SetString("EmployeeName", user.FullName);

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
    // YÖNETİCİ
    // =====================================================
    public IActionResult Yonetici()
    {
        var id = GetEmployeeId();
        if (id == null)
            return RedirectToAction(nameof(Login));

        // Manager görsün (Resul = Manager)
        if (!IsManager(id.Value))
            return RedirectToAction(nameof(Dashboard));

        ViewBag.EmployeeName = HttpContext.Session.GetString("EmployeeName");
        ViewBag.TodayAll = AttendanceRepository.GetTodayAll();

        return View();
    }

    private bool IsManager(int employeeId)
    {
        var emp = EmployeeRepository.GetById(employeeId);
        return emp?.Role == EmployeeRole.Manager;
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
