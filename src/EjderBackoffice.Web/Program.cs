using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

using Ejder.Infrastructure.Persistence;
using EjderBackoffice.Web.Authorization;

using Ejder.Application.Products.Services;
using Ejder.Application.Tours.Services;
using Ejder.Application.Reservations.Services;

var builder = WebApplication.CreateBuilder(args);

// MVC
builder.Services.AddControllersWithViews();

// SQL Server
var connStr = builder.Configuration.GetConnectionString("DefaultConnection")
             ?? throw new InvalidOperationException("ConnectionStrings:DefaultConnection bulunamadı.");

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(connStr, sql =>
    {
        sql.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null);
    });
});

// --------------------
// Application Services
// --------------------
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ITourProgramService, TourProgramService>();
builder.Services.AddScoped<ITourDocumentService, TourDocumentService>();
builder.Services.AddScoped<IReservationService, ReservationService>();

// Cookie Auth
builder.Services
    .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Auth/Login";
        options.LogoutPath = "/Auth/Logout";
        options.AccessDeniedPath = "/Auth/Denied";
        options.SlidingExpiration = true;
        options.ExpireTimeSpan = TimeSpan.FromHours(8);
        options.Cookie.HttpOnly = true;
        options.Cookie.SameSite = SameSiteMode.Lax;
        options.Cookie.SecurePolicy = builder.Environment.IsDevelopment()
            ? CookieSecurePolicy.SameAsRequest
            : CookieSecurePolicy.Always;
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("CanSeeFinance", p => p.RequireRole(Roles.Admin, Roles.Finance));
    options.AddPolicy("CanSeeOps",     p => p.RequireRole(Roles.Admin, Roles.Ops));
    options.AddPolicy("CanSeeSales",   p => p.RequireRole(Roles.Admin, Roles.Sales));
});

var app = builder.Build();

// Pipeline
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "admin",
    pattern: "admin/{controller=Dashboard}/{action=Index}/{id?}"
);

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Products}/{action=Index}/{id?}"
);

// Seed
using (var scope = app.Services.CreateScope())
{
    await EjderBackoffice.Web.Data.Seed.EnsureAdminAsync(scope.ServiceProvider);
}

app.Run();
