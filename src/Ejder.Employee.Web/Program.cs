using Microsoft.EntityFrameworkCore;
using Ejder.Infrastructure.Persistence;
using Ejder.Infrastructure.Repositories;
using Ejder.Domain.Repositories;

var builder = WebApplication.CreateBuilder(args);

// SQL Server
var connStr = builder.Configuration.GetConnectionString("DefaultConnection")
             ?? throw new InvalidOperationException("ConnectionStrings:DefaultConnection bulunamadı.");

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(connStr);
});

// Repositories
builder.Services.AddScoped<IAttendanceRepository, AttendanceRepository>();
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();

// MVC
builder.Services.AddControllersWithViews();

// 🔐 Session
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromHours(8);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

// 🧠 Dev hata ekranı
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

// Static files
app.UseStaticFiles();

app.UseRouting();

// 🔐 Session
app.UseSession();

// 🔐 (ileride aktif kullanacağız)
app.UseAuthentication();
app.UseAuthorization();

// Routing
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Portal}/{action=Login}/{id?}");

app.Run();
