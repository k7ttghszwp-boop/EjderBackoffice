using Microsoft.AspNetCore.Identity;

namespace EjderBackoffice.Data;

public static class SeedData
{
    public static async Task EnsureSeedAsync(IServiceProvider sp)
    {
        using var scope = sp.CreateScope();
        var roleMgr = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        var userMgr = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

        string[] roles = ["Admin", "IT", "Sales", "Finance", "Manager"];
        foreach (var r in roles)
            if (!await roleMgr.RoleExistsAsync(r))
                await roleMgr.CreateAsync(new IdentityRole(r));

        var adminEmail = "arda.alevekici@ejderturizm.com.tr"; // istersen değiştir
        var admin = await userMgr.FindByEmailAsync(adminEmail);

        if (admin == null)
        {
            admin = new ApplicationUser { UserName = adminEmail, Email = adminEmail, EmailConfirmed = true };
            await userMgr.CreateAsync(admin, "Ejder4818+"); // sonra değiştirirsin
            await userMgr.AddToRoleAsync(admin, "Admin");
        }
    }
}
