using Microsoft.AspNetCore.Authentication.Cookies;
using EjderBackoffice.Web.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// MVC
builder.Services.AddControllersWithViews();

// DB (SQLite)
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=ejder.db"));

// Auth (Cookie)
builder.Services
    .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Auth/Login";
        options.LogoutPath = "/Auth/Logout";
        options.AccessDeniedPath = "/Auth/Denied";

        // ReturnUrl düzgün çalışsın
        options.SlidingExpiration = true;
        options.ExpireTimeSpan = TimeSpan.FromHours(8);
    });

builder.Services.AddAuthorization();

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

// Auth middleware (sıra doğru)
app.UseAuthentication();
app.UseAuthorization();

// 🔒 BACKOFFICE (admin)
app.MapControllerRoute(
    name: "admin",
    pattern: "admin/{controller=Dashboard}/{action=Index}/{id?}"
);

// 🌍 PUBLIC
app.MapControllerRoute(
    name: "public",
    pattern: "{controller=Products}/{action=Index}/{id?}"
);

await EjderBackoffice.Web.Data.Seed.EnsureAdminAsync(app.Services);
app.Run();
