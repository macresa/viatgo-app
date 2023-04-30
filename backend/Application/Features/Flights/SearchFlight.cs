using Application.Features.Flights.Dtos;
using Application.Features.Flights.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Flights;
public static class SearchFlight
    {
        public static void SearchFlightEndpoint(this IEndpointRouteBuilder app)
        {
         app.MapGet("api/flight/search", async (ISender mediator, [AsParameters] SearchFlightRequest form) =>
            {
                return await mediator.Send(new SearchFlightQuery(form));
            })
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound);
        }

        public record SearchFlightQuery(SearchFlightRequest Form) : IRequest<IResult>;

        public class SearchFlightHandler : IRequestHandler<SearchFlightQuery, IResult>
        {
            private readonly IFlightRepository _repo;
            private readonly IMapper _mapper;

            public SearchFlightHandler(IFlightRepository repo, IMapper mapper)
            {
                this._repo = repo;
                this._mapper = mapper;
            }

            public async Task<IResult> Handle(SearchFlightQuery request, CancellationToken cancellationToken)
            {
               var flights = await _mapper.ProjectTo<FlightResponse>(_repo.Get().Where(f => 
                         f.Departure.City.Equals(request.Form.Departure) &&
                         f.Arrival.City.Equals(request.Form.Arrival) &&
                         f.Departure.Time.Date.Equals(request.Form.Date.Date))).ToListAsync();

               if (flights.Count == 0) return Results.NotFound("Sorry, we found no results.");

            return Results.Ok(flights);
        }
    }

     public record SearchFlightRequest
     (
        string Departure,
        string Arrival,
        DateTime Date
    );
}
