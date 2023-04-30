using Application.Features.Flights.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Application.Features.Flights;

public static class GetCities
{
    public static void GetCitiesEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapGet("api/flight/autocomplete/", async (ISender mediator, [FromQuery] string city) =>
        {
            return await mediator.Send(new GetCitiesQuery(city));
        })
        .Produces(StatusCodes.Status200OK);
    }

    public record GetCitiesQuery(string City) : IRequest<IResult>;

    public class GetCitiesHandler : IRequestHandler<GetCitiesQuery, IResult>
    {
        private readonly IFlightRepository _repo;

        public GetCitiesHandler(IFlightRepository repo)
        {
            this._repo = repo;
        }

        public async Task<IResult> Handle(GetCitiesQuery request, CancellationToken cancellationToken)
        {
            if (!request.City.IsNullOrEmpty()) { 

            var cities = new HashSet<string>();

            cities.UnionWith(await _repo.Get().Where(f => f.Departure.City
                                            .StartsWith(request.City))
                                            .Select(f => f.Departure.City)
                                            .Distinct()
                                            .ToListAsync());

            cities.UnionWith(await _repo.Get().Where(f => f.Arrival.City
                                            .StartsWith(request.City))
                                            .Select(f => f.Arrival.City)
                                            .Distinct()
                                            .ToListAsync());
                return Results.Ok(cities);
            }
            else
            {
                return Results.BadRequest();
            }     
        }
    }
}
