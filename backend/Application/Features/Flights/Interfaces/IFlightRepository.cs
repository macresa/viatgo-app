namespace Application.Features.Flights.Interfaces;
public interface IFlightRepository
{
    IQueryable<Entities.Flight> Get();
}

