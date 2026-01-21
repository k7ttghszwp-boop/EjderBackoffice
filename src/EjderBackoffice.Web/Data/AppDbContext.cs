using Microsoft.EntityFrameworkCore;
using Ejder.Core.Models;

namespace EjderBackoffice.Web.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }

    public DbSet<Reservation> Reservations => Set<Reservation>();
    public DbSet<Product> Products => Set<Product>();

    // ✅ Backoffice login için
    public DbSet<BackofficeUser> BackofficeUsers => Set<BackofficeUser>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<BackofficeUser>()
            .HasIndex(x => x.Email)
            .IsUnique();
    }
}
