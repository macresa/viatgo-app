using Application.Entities;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Application.Features.Auth.Dtos;
using Application.Features.Auth.Services;

namespace Application.Features.Auth;
public static class Login
{
    public static void LoginEndpoint(this IEndpointRouteBuilder app)
    {
     app.MapPost("api/login", async ([FromBody] LoginRequest login, 
        ISender mediator, IValidator <LoginRequest> validator) =>
        {
            var validationResult = validator.Validate(login);
            if (validationResult.IsValid)
            {
                return await mediator.Send(new LoginCommand(login));
            }
            return Results.ValidationProblem(validationResult.ToDictionary());
        })
        .AllowAnonymous()
        .Produces(StatusCodes.Status400BadRequest)
        .Produces(StatusCodes.Status200OK);
    }
    public record LoginCommand(LoginRequest Login) : IRequest<IResult>;

    public class LoginCommandHandler : IRequestHandler<LoginCommand, IResult>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly TokenService _token;
        public LoginCommandHandler(UserManager<ApplicationUser> userManager,
                                   TokenService token)
        {
            this._userManager = userManager;
            this._token = token;
        }
        public async Task<IResult> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var result = await _userManager.FindByNameAsync(request.Login.userName);

            if (result != null)
            {
                if (await _userManager.CheckPasswordAsync(result, request.Login.password))
                {
                    return _token.BuildToken(request.Login);
                }
                else
                {
                    return Results.BadRequest("Invalid Password");
                }
            }

            else
            {
                return Results.BadRequest("Username not found");
            }

        }

    }


}
