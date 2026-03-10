using Microsoft.EntityFrameworkCore;
using Ejder.Infrastructure.Persistence;
using Ejder.Infrastructure.Repositories;
using Ejder.Domain.Repositories;
using Ejder.Application.Reservations.Services;

var builder = WebApplication.CreateBuilder(args);

// SQL Server
var connStr = builder.Configuration.GetConnectionString("DefaultConnection")
             ?? throw new InvalidOperationException("ConnectionStrings:DefaultConnection bulunamadı.");

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(connStr);
});

// Repositories
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IReservationRepository, ReservationRepository>();

// Services
builder.Services.AddScoped<IReservationService, ReservationService>();

builder.Services.AddControllersWithViews();

var app = builder.Build();

app.UseStaticFiles();
app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Leads}/{action=Index}/{id?}");

app.Run();
