using System.ComponentModel.DataAnnotations;

namespace Application.Entities;
public class Booking
{
    [Key]
    public Guid Id { get; set; }
    public required int DepartureId { get; set; }
    public int? ReturnId { get; set; }
    public Flight? Departure { get; set; }
    public Flight? Return { get; set; }

    public required string UserName { get; set; }
    public required string ApplicationUserId { get; set; }
    public virtual ApplicationUser? ApplicationUser { get; set; }
}



