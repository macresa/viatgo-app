using Application.Entities;

namespace Application.Features.Bookings.Interfaces;
public interface IBookingRepository
{
    void Create(Booking booking);
    IQueryable<Booking> Get();
    Task SaveChangesAsync();
}


