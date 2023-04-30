using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Application.Features.Flights;
using Application.Features.Auth.Dtos;
using Application.Features.Auth.Services;
using Application.Features.Flights.Profiles;

namespace Application;
public static class DependencyInjection
{
    public static IServiceCollection AddApplicationLayer(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMediatR(cfg => 
        cfg.RegisterServicesFromAssembly(typeof(GetFlightById).Assembly));
        services.AddAutoMapper(typeof(FlightProfile));
        services.AddValidatorsFromAssemblyContaining<LoginValidator>();

        services.AddScoped<TokenService, TokenService>();

        services.AddAuthorization()
            .AddAuthentication(opt => {
               opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
               opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
               opt.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
             })
            .AddJwtBearer(opt =>
               opt.TokenValidationParameters = new TokenValidationParameters
               {
                  ValidateIssuer = true,
                  ValidateAudience = true,
                  ValidateLifetime = true,
                  ValidateIssuerSigningKey = true,
                  ValidIssuer = configuration["Jwt:Issuer"],
                  ValidAudience = configuration["Jwt:Audience"],
                  IssuerSigningKey = new SymmetricSecurityKey(
                  Encoding.UTF8.GetBytes(configuration["Jwt:Secret_Key"])),
                  ClockSkew = TimeSpan.Zero
               });

        return services;
    }

}
