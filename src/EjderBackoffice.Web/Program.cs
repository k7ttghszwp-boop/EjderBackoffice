using Microsoft.AspNetCore.Authentication.Cookies;
using EjderBackoffice.Web.Data;
using Microsoft.EntityFrameworkCore;
var builder = WebApplication.CreateBuilder(args);

// ✅ Services (HEPSİ build'den önce)
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=ejder.db"));


builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(o =>
    {
        o.LoginPath = "/Auth/Login";
        o.LogoutPath = "/Auth/Logout";
    });

builder.Services.AddAuthorization();

var app = builder.Build();

// ✅ Pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// ✅ Auth middleware sırası önemli
app.UseAuthentication();
app.UseAuthorization();

// 🔒 BACKOFFICE
app.MapControllerRoute(
    name: "admin",
    pattern: "admin/{controller=Dashboard}/{action=Index}/{id?}"
);

// 🌍 PUBLIC
app.MapControllerRoute(
    name: "public",
    pattern: "{controller=Products}/{action=Index}/{id?}"
);


app.Run();
