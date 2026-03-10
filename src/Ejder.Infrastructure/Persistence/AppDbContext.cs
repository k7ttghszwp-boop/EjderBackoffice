using Ejder.Domain.Entities;
using Ejder.Domain.Products;
using Ejder.Domain.Reservations;
using Ejder.Domain.Tours;
using Ejder.Domain.HR;
using Microsoft.EntityFrameworkCore;

namespace Ejder.Infrastructure.Persistence;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }

    // ✅ DbSets
    public DbSet<Product> Products => Set<Product>();
    public DbSet<BackofficeUser> BackofficeUsers => Set<BackofficeUser>();
    public DbSet<Reservation> Reservations => Set<Reservation>();
    public DbSet<TourDocument> TourDocuments => Set<TourDocument>();
    public DbSet<Employee> Employees => Set<Employee>();
    public DbSet<Department> Departments => Set<Department>();
    public DbSet<Attendance> Attendances => Set<Attendance>();
    public DbSet<Category> Categories => Set<Category>();
    public DbSet<Tour> Tours => Set<Tour>();
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Apply Entity Configurations
        modelBuilder.ApplyConfiguration(new Ejder.Infrastructure.Configurations.CategoryConfiguration());
        modelBuilder.ApplyConfiguration(new Ejder.Infrastructure.Configurations.TourConfiguration());

        // ✅ Product
        modelBuilder.Entity<Product>(entity =>
        {
            entity.Property(x => x.Price)
                  .HasColumnType("decimal(18,2)");

            entity.Property(x => x.Name)
                  .HasMaxLength(200)
                  .IsRequired();

            entity.Ignore(x => x.Highlights);
            entity.Ignore(x => x.Included);
            entity.Ignore(x => x.NotIncluded);
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

        // ✅ Reservation
        modelBuilder.Entity<Reservation>(entity =>
        {
            entity.Property(x => x.UnitPrice)
                  .HasColumnType("decimal(18,2)");
            entity.Property(x => x.AmountTry)
                  .HasColumnType("decimal(18,2)");
        });

        // ✅ Employee
        modelBuilder.Entity<Employee>(entity =>
        {
            entity.Property(x => x.Email)
                  .HasMaxLength(256)
                  .IsRequired();
            
            entity.HasIndex(x => x.Email)
                  .IsUnique();
        });
    }
}