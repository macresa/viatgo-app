using Application.Entities;
using Application.Features.Bookings.Interfaces;
using Application.Features.Flights.Interfaces;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
namespace Infrastructure;
public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureLayer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IFlightRepository, FlightRepository>();
            services.AddScoped<IBookingRepository,BookingRepository>();
            services.AddDbContext<DataContext>(opt => opt
                .UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>(
            opt =>
            {
                 opt.Password.RequireDigit = false;
                 opt.Password.RequireLowercase = true;
                 opt.Password.RequireNonAlphanumeric = false;
                 opt.Password.RequireUppercase = false;
                 opt.Password.RequiredLength = 6;
                 opt.Password.RequiredUniqueChars = 0;
            })
            .AddEntityFrameworkStores<DataContext>()
            .AddDefaultTokenProviders();
        return services;
        }
    }

