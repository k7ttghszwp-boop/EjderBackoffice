using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EjderBackoffice.Web.Data;

public static class Seed
{
    public static async Task EnsureAdminAsync(IServiceProvider services)
    {
        using var scope = services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        // DB ve migration
        await db.Database.MigrateAsync();

        var email = "admin@ejderturizm.com.tr";

        var exists = await db.BackofficeUsers.AnyAsync(x => x.Email == email);
        if (exists) return;

        var hasher = new PasswordHasher<BackofficeUser>();

        var user = new BackofficeUser
        {
            Email = email,
            Role = "Admin",
            IsActive = true
        };

        user.PasswordHash = hasher.HashPassword(user, "Ejder4818+");

        db.BackofficeUsers.Add(user);
        await db.SaveChangesAsync();
    }
}
