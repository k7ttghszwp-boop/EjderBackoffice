var builder = WebApplication.CreateBuilder(args);

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
