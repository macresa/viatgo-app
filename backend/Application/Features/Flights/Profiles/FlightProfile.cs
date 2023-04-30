using Application.Features.Flights.Dtos;
using AutoMapper;

namespace Application.Features.Flights.Profiles;
public class FlightProfile : Profile
{
    public FlightProfile()
    {
        CreateMap<Entities.Flight, FlightResponse>();
    }
}
