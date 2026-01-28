using Ejder.Infrastructure.Persistence;
using Ejder.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EjderBackoffice.Web.Data;

public static class Seed
{
    public static async Task EnsureAdminAsync(IServiceProvider services)
    {
        var db = services.GetRequiredService<AppDbContext>();

        await db.Database.MigrateAsync();

        var email = "admin@ejderturizm.com.tr";

        if (await db.BackofficeUsers.AnyAsync(x => x.Email == email))
            return;

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
