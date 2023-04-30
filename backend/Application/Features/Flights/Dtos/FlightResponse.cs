namespace Application.Features.Flights.Dtos;
public record FlightResponse(  
   int Id,
   string Airline,
   double Price,
   string DepartureCity,
   DateTime DepartureTime,
   string ArrivalCity,
   DateTime ArrivalTime
);
