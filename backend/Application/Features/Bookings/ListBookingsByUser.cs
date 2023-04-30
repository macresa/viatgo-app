using Application.Features.Bookings.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Bookings;

public static class ListBookingsByUser
{
   public static void ListBookingsByUserEndpoint(this IEndpointRouteBuilder app)
   {
        app.MapGet("api/flight/bookings", async (ISender mediator, string user) =>
        {
            return await mediator.Send(new ListBookingsByUserQuery(user));
        })
            .RequireAuthorization()
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound);
    }
   public record ListBookingsByUserQuery(string User) : IRequest<IResult>;

    public class ListBookingsByUserHandler : IRequestHandler<ListBookingsByUserQuery, IResult>
    {
        private readonly IBookingRepository _repo;
        public ListBookingsByUserHandler(IBookingRepository repo)
        {
            _repo = repo;
        }

        public async Task<IResult> Handle(ListBookingsByUserQuery request, CancellationToken cancellationToken)
        {
            var bookings = await _repo.Get().Where(b => b.UserName == request.User)
                .Select(b => new
                {
                    b.Id,
                    b.Departure,
                    b.Return
                }).ToListAsync(); 

            var bookingsResponse = bookings.Select(
                b => new
                {
                    b.Id,
                    DepartureFlight = new
                    {
                        b.Departure!.Id,
                        b.Departure.Airline,
                        b.Departure.Price,
                        b.Departure.Departure,
                        b.Departure.Arrival
                    },

                    ReturnFlight = new
                    {
                        b.Return!.Id,
                        b.Return!.Airline,
                        b.Return!.Price,
                        b.Return!.Departure,
                        b.Return!.Arrival
                    }
                }
                ).ToList();
            
            return Results.Ok(bookingsResponse);
        }
    }
}