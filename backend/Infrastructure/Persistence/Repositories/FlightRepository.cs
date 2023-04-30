using Application.Entities;
using Application.Features.Flights.Interfaces;
using Infrastructure.Persistence;

namespace Infrastructure.Persistence.Repositories;

public class FlightRepository : IFlightRepository
{
    private readonly DataContext _db;
    public FlightRepository(DataContext db) => _db = db;

    public IQueryable<Flight> Get()
        => _db.Flights;
}
