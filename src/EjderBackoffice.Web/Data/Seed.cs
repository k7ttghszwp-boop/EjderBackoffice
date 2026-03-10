using Ejder.Infrastructure.Persistence;
using Ejder.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using BC = BCrypt.Net.BCrypt;

namespace EjderBackoffice.Web.Data;

public static class Seed
{
    public static async Task EnsureAdminAsync(IServiceProvider services)
    {
        var db = services.GetRequiredService<AppDbContext>();

        await db.Database.MigrateAsync();

        var email = "admin@ejderturizm.com.tr";

        var user = await db.BackofficeUsers.FirstOrDefaultAsync(x => x.Email == email);
        if (user == null)
        {
            user = new BackofficeUser
            {
                Email = email,
                Role = "Admin",
                IsActive = true
            };

            db.BackofficeUsers.Add(user);
        }

        // Parolayı her seferinde BCrypt ile güncelle
        user.PasswordHash = BC.HashPassword("Ejder4818+");

        await db.SaveChangesAsync();
    }
}
