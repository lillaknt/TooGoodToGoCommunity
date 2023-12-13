using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.LogicInterfaces;
using Domain.DTOs;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly IConfiguration config;
    private readonly IUserLogic logic;

    public AuthController(IConfiguration config, IUserLogic logic)
    {
        this.config = config;
        this.logic = logic;
    }


    [HttpPost]
    [Route("login")]
    public async Task<ActionResult> Login([FromBody] UserLoginDto dto)
    {
        try
        {
            // Validate user credentials and retrieve user information

            var user = await logic.ValidateUser(dto.Email, dto.Password);

            // Generate JWT token for the authenticated user

            var token = GenerateJwt(user);
            // Return the JWT token in the response

            return Ok(token);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    private List<Claim> GenerateClaims(User user)
    {
        // Define the claims for the JWT token based on user information

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, config["Jwt:Subject"]),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Name, user.FirstName),

            // Additional claims based on user properties

            new Claim(ClaimTypes.PostalCode,
                user.PostCode?.ToString() ?? string.Empty), // use empty string if postcode is null
            new Claim(ClaimTypes.UserData, user.ItemsPurchased?.ToString() ?? string.Empty),
            new Claim(ClaimTypes.Thumbprint, user.CO2Saved?.ToString() ?? string.Empty)
        };
        return claims.ToList();
    }

    private string GenerateJwt(User user)
    {
        // Generate JWT token using user claims and configuration settings

        var claims = GenerateClaims(user);

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]));
        var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

        var header = new JwtHeader(signIn);

        var payload = new JwtPayload(
            config["Jwt:Issuer"],
            config["Jwt:Audience"],
            claims,
            null,
            DateTime.UtcNow.AddMinutes(60));

        var token = new JwtSecurityToken(header, payload);
        // Serialize the token into a string

        var serializedToken = new JwtSecurityTokenHandler().WriteToken(token);
        return serializedToken;
    }
}