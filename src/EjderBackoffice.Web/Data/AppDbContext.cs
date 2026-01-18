using Microsoft.EntityFrameworkCore;
using Ejder.Core.Models;

namespace EjderBackoffice.Web.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }

    public DbSet<Reservation> Reservations => Set<Reservation>();
    public DbSet<Product> Products => Set<Product>();
}
