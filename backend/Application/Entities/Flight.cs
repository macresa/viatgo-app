using System.ComponentModel.DataAnnotations;

namespace Application.Entities;

public class Flight
{
    public int Id { get; set; }
    public required string Airline { get; set; }
    public double Price { get; set; }
    public required Place Departure { get; set; }
    public required Place Arrival { get; set; }
 
    public virtual IList<Booking>? Departures { get; set; }
    public virtual IList<Booking>? Returns { get; set; }
}

public record Place(string City,DateTime Time);