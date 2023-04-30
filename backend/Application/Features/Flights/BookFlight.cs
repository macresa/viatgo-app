using Application.Entities;
using Application.Features.Bookings.Interfaces;
using Application.Features.Flights.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Flights;

public static class BookFlight
   {
        public static void BookFlightEndpoint(this IEndpointRouteBuilder app)
        {
          app.MapPost("api/flight/booking", async (ISender mediator, [FromBody] BookingRequest form) =>
        {
            return await mediator.Send(new BookFlightCommand(form));
        })
        .RequireAuthorization()
        .Produces(StatusCodes.Status201Created)
        .Produces(StatusCodes.Status404NotFound);
        }

      public record BookFlightCommand(BookingRequest Form) : IRequest<IResult>;

      public class BookFlightHandler : IRequestHandler<BookFlightCommand, IResult>
      {
        private readonly IFlightRepository _flightRepo;
        private readonly IBookingRepository _bookingRepo;
        private readonly UserManager<ApplicationUser> _userManager;

        public BookFlightHandler(IFlightRepository flightRepo, 
            IBookingRepository bookingRepo,
            UserManager<ApplicationUser> userManager)
            {
                this._flightRepo = flightRepo;
                this._bookingRepo = bookingRepo;
                this._userManager = userManager;
            }

         public async Task<IResult> Handle(BookFlightCommand request, CancellationToken cancellationToken)
         {
            if (request.Form.IdDeparture == request.Form.IdReturn) return Results.BadRequest();

            var user = await _userManager.FindByNameAsync(request.Form.UserName);

            if (user == null) return Results.BadRequest("Username was not found");

            var flightDeparture = await _flightRepo.Get().Where(f => f.Id == request.Form.IdDeparture)
                                               .AsNoTracking().FirstOrDefaultAsync();

            if (flightDeparture == null) return Results.NotFound("Departure Id was not found");

            if (request.Form.IdReturn.HasValue)
            {
                var flightReturn = await _flightRepo.Get().Where(f => f.Id == request.Form.IdReturn)
                                       .AsNoTracking().FirstOrDefaultAsync();
                if (flightReturn == null)
                    return Results.NotFound("Return Id was not found");
                
                _bookingRepo.Create(new Booking
                {
                    DepartureId = request.Form.IdDeparture,
                    ReturnId = request.Form.IdReturn,
                    ApplicationUserId = user.Id,
                    UserName = user.UserName!
                });
            }
            else
            {
                if (flightDeparture == null)
                    return Results.NotFound("Departure Id was not found");
                 _bookingRepo.Create(new Booking
                {
                    DepartureId = request.Form.IdDeparture,
                    ApplicationUserId = user.Id,
                    UserName = user.UserName!
                });
            }
            await _bookingRepo.SaveChangesAsync();

            return Results.Created("flight", null);
         }
      }

    public record BookingRequest(int IdDeparture,int? IdReturn, string UserName);

   }

