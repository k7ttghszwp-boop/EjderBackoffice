using Microsoft.EntityFrameworkCore;
using EjderBackoffice.Web.Models;

namespace EjderBackoffice.Web.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }

    public DbSet<Reservation> Reservations => Set<Reservation>();
    public DbSet<Tour> Tours => Set<Tour>();
}
