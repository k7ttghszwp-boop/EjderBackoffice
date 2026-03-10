using Microsoft.AspNetCore.Mvc;
using Ejder.Domain.Repositories;

namespace Ejder.Employee.Web.Controllers;

public class AdminController : Controller
{
    private readonly IAttendanceRepository _attendanceRepo;
    private readonly IEmployeeRepository _employeeRepo;
    private readonly IRepository<Ejder.Domain.HR.Department> _deptRepo;

    public AdminController(
        IAttendanceRepository attendanceRepo, 
        IEmployeeRepository employeeRepo,
        IRepository<Ejder.Domain.HR.Department> deptRepo)
    {
        _attendanceRepo = attendanceRepo;
        _employeeRepo = employeeRepo;
        _deptRepo = deptRepo;
    }

    public async Task<IActionResult> Dashboard()
    {
        var list = await _attendanceRepo.GetTodayAllAsync();
        return View(list);
    }

    public async Task<IActionResult> Employees()
    {
        var emps = await _employeeRepo.GetAllAsync();
        ViewBag.Departments = await _deptRepo.GetAllAsync();
        return View(emps);
    }
}

