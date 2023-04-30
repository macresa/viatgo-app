using Microsoft.AspNetCore.Identity;

namespace Application.Entities;
    public class ApplicationUser : IdentityUser
    {
      public virtual IList<Booking>? Bookings { get; set; }
    
    }

