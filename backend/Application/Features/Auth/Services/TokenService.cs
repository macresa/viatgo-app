using Application.Features.Auth.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Application.Features.Auth.Services;

public class TokenService
{
    private readonly IConfiguration _config;
    public TokenService(IConfiguration configuration)
    {
        _config = configuration;
    }

    public IResult BuildToken(LoginRequest login)
    {
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.UniqueName, login.userName)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Secret_Key"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var expiration = DateTime.UtcNow.AddMinutes(30);

        JwtSecurityToken token = new JwtSecurityToken(
            issuer: _config["Jwt:Issuer"],
            audience: _config["Jwt:Audience"],
            claims: claims,
            expires: expiration,
            signingCredentials: creds);
        return Results.Ok(new
        {
            token = new JwtSecurityTokenHandler().WriteToken(token),
            expiration
        });
    }
}



