using Microsoft.EntityFrameworkCore;
using Ejder.Infrastructure.Persistence;
using Ejder.Infrastructure.Repositories;
using Ejder.Domain.Repositories;
using Ejder.Application.Products.Services;
using Ejder.Application.Tours.Services;
using Ejder.Application.Reservations.Services;

var builder = WebApplication.CreateBuilder(args);

// MVC
builder.Services.AddControllersWithViews();
builder.Services.AddHttpClient();

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
builder.Services.AddScoped<IProductRepository, ProductRepository>();

// =========================
// APPLICATION SERVICES
// =========================
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ITourProgramService, TourProgramService>();
builder.Services.AddScoped<ITourDocumentService, TourDocumentService>();
builder.Services.AddScoped<IReservationService, ReservationService>();

var app = builder.Build();

// Pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Public'te Authorize kullanmıyorsan bunu da kaldırabilirsin.
// app.UseAuthorization();

app.MapControllerRoute(
    name: "localized",
    pattern: "{lang:regex(tr|en)}/{controller=Home}/{action=Index}/{id?}"
);

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
);

app.Run();
