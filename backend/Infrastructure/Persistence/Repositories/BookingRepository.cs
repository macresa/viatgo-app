using Application.Entities;
using Application.Features.Bookings.Interfaces;

namespace Infrastructure.Persistence.Repositories;
public class BookingRepository : IBookingRepository
{
    private readonly DataContext _db;
    public BookingRepository(DataContext db) => _db = db;

    public IQueryable<Booking> Get() => _db.Bookings;
    public void Create(Booking booking)
    {
        _db.Add(booking);
    }

    public async Task SaveChangesAsync()
        => await _db.SaveChangesAsync();

}
