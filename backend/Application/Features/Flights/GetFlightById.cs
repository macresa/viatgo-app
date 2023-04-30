using Application.Features.Flights.Dtos;
using Application.Features.Flights.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Flights;

public static class GetFlightById
{
    public static void GetFlightByIdEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapGet("api/flight/search/{id}", async (ISender mediator, int id) =>
        {
            return await mediator.Send(new GetFlightByIdQuery(id));
        })
        .Produces(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);
    }

    public record GetFlightByIdQuery(int Id) : IRequest<IResult>;

    public class GetFlightByIdHandler : IRequestHandler<GetFlightByIdQuery, IResult>
    {
        private readonly IFlightRepository _repo;
        private readonly IMapper _mapper;

        public GetFlightByIdHandler(IFlightRepository repo, IMapper mapper)
        {
            this._repo = repo;
            this._mapper = mapper;
        }

        public async Task<IResult> Handle(GetFlightByIdQuery request, CancellationToken cancellationToken)
        {
            var flight = await _mapper.ProjectTo<FlightResponse>(_repo.Get()
                                       .Where(f => f.Id == request.Id)).FirstOrDefaultAsync();

            if (flight == null) return Results.NotFound($"Flight {request.Id} was not found.");

            return Results.Ok(flight);          
        }
    }
}