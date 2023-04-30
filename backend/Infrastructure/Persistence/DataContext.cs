using Application.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

public class DataContext : IdentityDbContext<ApplicationUser>
{
    public DataContext(DbContextOptions<DataContext> options) : base(options) { }

    public DbSet<Flight> Flights { get; set; }
    public DbSet<Booking> Bookings { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Flight>().OwnsOne(f => f.Departure);
        modelBuilder.Entity<Flight>().OwnsOne(f => f.Arrival);

        modelBuilder.Entity<Booking>().HasOne(b => b.Departure)
                                       .WithMany(f => f.Departures).HasForeignKey
                                       (b => b.DepartureId).IsRequired();

        modelBuilder.Entity<Booking>().HasOne(b => b.Return)
                                      .WithMany(f => f.Returns)
                                      .HasForeignKey(b => b.ReturnId);

        modelBuilder.Entity<Booking>().HasKey(b => b.Id);

        modelBuilder.Entity<Booking>().HasOne(b => b.ApplicationUser)
                                      .WithMany(u => u.Bookings)
                                      .HasForeignKey(b => b.ApplicationUserId)
                                      .IsRequired();

        modelBuilder.Entity<Booking>().Property(b => b.ApplicationUserId)
                                      .HasColumnName("UserId");
    }

}
