namespace Ejder.Employee.Web.Models;

public class AdminEmployeeRow
{
    public string EmployeeName { get; set; } = "";
    public DateTime? CheckIn { get; set; }
    public DateTime? CheckOut { get; set; }

    public string Status =>
        CheckIn == null ? "Gelmedi"
        : CheckOut == null ? "İçeride"
        : "Çıkış Yaptı";
}
