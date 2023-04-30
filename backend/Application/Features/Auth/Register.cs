using Application.Entities;
using Application.Features.Auth.Dtos;
using Application.Features.Auth.Services;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace Application.Features.Auth;
public static class Register
{
    public static void RegisterEndpoint(this IEndpointRouteBuilder app)
    {
     app.MapPost("api/register", async ([FromBody] LoginRequest login, 
        ISender mediator, IValidator<LoginRequest> validator) =>
        {
            var validationResult = validator.Validate(login);
            if (validationResult.IsValid)
            {
               return await mediator.Send(new RegisterCommand(login));
            }
            return Results.ValidationProblem(validationResult.ToDictionary());
        })
         .AllowAnonymous()
         .Produces(StatusCodes.Status400BadRequest)
         .Produces(StatusCodes.Status200OK);
    }
    public record RegisterCommand(LoginRequest Login) : IRequest<IResult>;

    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, IResult>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly TokenService _token;
        public RegisterCommandHandler(UserManager<ApplicationUser> userManager,                                  
                                   TokenService token)                                 
        {
            this._userManager = userManager;
            this._token = token;
        }
        public async Task<IResult> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            var user = new ApplicationUser 
            { 
              UserName = request.Login.userName
            };

            var result = await _userManager.CreateAsync(user, request.Login.password);

            if (result.Succeeded)
            {
                return _token.BuildToken(request.Login);
            } 
            else
            {
                return Results.BadRequest("Username already exist");
            }        
        }
    }    
}

