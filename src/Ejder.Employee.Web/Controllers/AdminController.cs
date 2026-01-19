using Microsoft.AspNetCore.Mvc;
using Ejder.Core.HR;

namespace Ejder.Employee.Web.Controllers;

public class AdminController : Controller
{
    public IActionResult Dashboard()
    {
        var list = AttendanceRepository.GetTodayAll();
        return View(list);
    }
}
