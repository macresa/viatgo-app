using Application;
using Application.Features.Bookings;
using Application.Features.Flights;
using Application.Features.Auth;
using Infrastructure;
using Infrastructure.Persistence;
using Api;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer(); 
builder.Services.AddSwaggerGen();

builder.Services.AddInfrastructureLayer(builder.Configuration);
builder.Services.AddApplicationLayer(builder.Configuration);

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        policy =>
        {
            policy.WithOrigins("http://localhost:4000").AllowAnyMethod().AllowAnyHeader();
        });
});


var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<DataContext>();
    context.Database.EnsureDeleted();
    context.Database.EnsureCreated();
    Seeder.Seed(context);
}

app.UseCors();

app.UseAuthentication();
app.UseAuthorization();

app.BookFlightEndpoint();
app.GetCitiesEndpoint();
app.GetFlightByIdEndpoint();
app.GetFlightsByCitiesEndpoint();
app.SearchFlightEndpoint();

app.ListBookingsByUserEndpoint();

app.RegisterEndpoint();
app.LoginEndpoint();


app.UseSwaggerUI().UseSwagger();

app.Run();