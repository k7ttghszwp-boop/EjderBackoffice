using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Ejder.Domain.HR;
using Ejder.Domain.Repositories;

namespace Ejder.Employee.Web.Controllers;

public class PortalController : Controller
{
    private readonly IEmployeeRepository _employeeRepo;
    private readonly IAttendanceRepository _attendanceRepo;

    public PortalController(IEmployeeRepository employeeRepo, IAttendanceRepository attendanceRepo)
    {
        _employeeRepo = employeeRepo;
        _attendanceRepo = attendanceRepo;
    }

    // =====================================================
    // LOGIN
    // =====================================================
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(string email, string password)
    {
        var user = await _employeeRepo.LoginAsync(email, password);

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
    public async Task<IActionResult> Dashboard()
    {
        var id = GetEmployeeId();
        if (id == null)
            return RedirectToAction(nameof(Login));

        ViewBag.EmployeeName = HttpContext.Session.GetString("EmployeeName");
        ViewBag.Today = await _attendanceRepo.GetTodayByEmployeeIdAsync(id.Value);

        return View();
    }

    // =====================================================
    // YÖNETİCİ
    // =====================================================
    public async Task<IActionResult> Yonetici()
    {
        var id = GetEmployeeId();
        if (id == null)
            return RedirectToAction(nameof(Login));

        if (!await IsManager(id.Value))
            return RedirectToAction(nameof(Dashboard));

        ViewBag.EmployeeName = HttpContext.Session.GetString("EmployeeName");
        ViewBag.TodayAll = await _attendanceRepo.GetTodayAllAsync();
        ViewBag.Employees = await _employeeRepo.GetAllAsync();

        return View();
    }

    private async Task<bool> IsManager(int employeeId)
    {
        var emp = await _employeeRepo.GetByIdAsync(employeeId);
        return emp?.Role == EmployeeRole.Manager;
    }

    // =====================================================
    // GİRİŞ / ÇIKIŞ
    // =====================================================
    [HttpPost]
    public async Task<IActionResult> CheckIn()
    {
        var id = GetEmployeeId();
        if (id != null)
        {
            var attendance = await _attendanceRepo.GetTodayByEmployeeIdAsync(id.Value);
            if (attendance == null)
            {
                attendance = new Attendance
                {
                    EmployeeId = id.Value,
                    Date = DateTime.Today,
                    CheckIn = DateTime.Now
                };
                await _attendanceRepo.AddAsync(attendance);
            }
            else if (attendance.CheckIn == null)
            {
                attendance.CheckIn = DateTime.Now;
                _attendanceRepo.Update(attendance);
            }
            await _attendanceRepo.SaveChangesAsync();
        }

        return RedirectToAction(nameof(Dashboard));
    }

    [HttpPost]
    public async Task<IActionResult> CheckOut()
    {
        var id = GetEmployeeId();
        if (id != null)
        {
            var attendance = await _attendanceRepo.GetTodayByEmployeeIdAsync(id.Value);
            if (attendance != null && attendance.CheckOut == null)
            {
                attendance.CheckOut = DateTime.Now;
                _attendanceRepo.Update(attendance);
                await _attendanceRepo.SaveChangesAsync();
            }
        }

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

