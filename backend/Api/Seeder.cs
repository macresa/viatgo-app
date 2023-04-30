using Application.Entities;
using Infrastructure.Persistence;

namespace Api;
    public static class Seeder
    {
        public static void Seed(this DataContext context)
        {
         context.Add(new Flight
         {
             Airline = "Aerolineas Argentinas",
             Price = 120000,
             Departure = new Place("Buenos Aires", DateTime.Parse("2023-04-15 12:00")),
             Arrival = new Place("Cordoba", DateTime.Parse("2023-04-15 14:10"))
         });
         context.Add(new Flight
         {
             Airline = "Flybondi",
             Price = 110600,
             Departure = new Place("Cordoba", DateTime.Parse("2023-04-15 18:00")),
             Arrival = new Place("Buenos Aires", DateTime.Parse("2023-04-15 20:40"))
         });
          context.SaveChanges();
            
        }
    }
