using Ejder.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Ejder.Infrastructure.Persistence;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }

    // ✅ DbSets
    public DbSet<Product> Products => Set<Product>();
    public DbSet<BackofficeUser> BackofficeUsers => Set<BackofficeUser>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // ✅ Product
        modelBuilder.Entity<Product>(entity =>
        {
            entity.Property(x => x.Price)
                  .HasColumnType("decimal(18,2)");

            entity.Property(x => x.Name)
                  .HasMaxLength(200)
                  .IsRequired();
        });

        // ✅ BackofficeUser
        modelBuilder.Entity<BackofficeUser>(entity =>
        {
            entity.Property(x => x.Email)
                  .HasMaxLength(256)
                  .IsRequired();

            entity.HasIndex(x => x.Email)
                  .IsUnique();

            entity.Property(x => x.PasswordHash)
                  .HasMaxLength(500)
                  .IsRequired();

            entity.Property(x => x.Role)
                  .HasMaxLength(50)
                  .IsRequired();

            entity.Property(x => x.IsActive)
                  .HasDefaultValue(true);
        });
    }
}