using Application.Features.Flights.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Flights;

public static class GetFlightsByCities
{
    public static void GetFlightsByCitiesEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapGet("api/flight/cities", async (ISender mediator, [AsParameters] GetFlightsByCitiesRequest cities) =>
        {
            return await mediator.Send(new GetFlightsByCitiesQuery(cities));
        })
        .Produces(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);
    }

    public record GetFlightsByCitiesQuery(GetFlightsByCitiesRequest Cities) : IRequest<IResult>;

    public class GetFlightsByCitiesHandler : IRequestHandler<GetFlightsByCitiesQuery, IResult>
    {
        private readonly IFlightRepository _repo;

        public GetFlightsByCitiesHandler(IFlightRepository repo)
        {
            this._repo = repo;
        }

        public async Task<IResult> Handle(GetFlightsByCitiesQuery request, CancellationToken cancellationToken)
        {
            var flights = await _repo.Get().Where(f => f.Departure.City.Equals(request.Cities.DepartureCity) 
                                  && f.Arrival.City.Equals(request.Cities.ArrivalCity))
                                          .AsNoTracking().ToListAsync();

            return Results.Ok(flights);
        }
        
    }
    public record GetFlightsByCitiesRequest(string DepartureCity, string ArrivalCity);
}
