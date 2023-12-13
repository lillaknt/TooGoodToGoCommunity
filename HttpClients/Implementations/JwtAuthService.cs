using System.Security.Claims;
using System.Text;
using System.Text.Json;
using Domain.DTOs;
using Domain.Models;
using HttpClients.ClientInterfaces;

namespace HttpClients.Implementations;

/// Implementation of the authentication service using JWT.
public class JwtAuthService : IAuthService
{
    private readonly HttpClient client = new();

    // Static field to hold the JWT token received from the WebAPI
    public static string? Jwt { get; private set; } = "";
    // Action to notify when the authentication state changes

    public Action<ClaimsPrincipal> OnAuthStateChanged { get; set; } = null!;

    public async Task LoginAsync(string email, string password)
    {
        // Create a UserLoginDto from the provided email and password

        UserLoginDto userLoginDto = new()
        {
            Email = email,
            Password = password
        };
        // Serialize the UserLoginDto to JSON

        var userAsJson = JsonSerializer.Serialize(userLoginDto);
        StringContent content = new(userAsJson, Encoding.UTF8, "application/json");
        // Send a POST request to the authentication endpoint

        var response = await client.PostAsync("https://localhost:7092/auth/login", content);
        var responseContent = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode) throw new Exception(responseContent);

        var token = responseContent;
        Jwt = token;

        var principal = CreateClaimsPrincipal();
        // Invoke the authentication state change callback

        OnAuthStateChanged.Invoke(principal);
    }

    public Task LogoutAsync()
    {
        // Clear the JWT token and create an empty ClaimsPrincipal

        Jwt = null;
        ClaimsPrincipal principal = new();
        OnAuthStateChanged.Invoke(principal);
        return Task.CompletedTask;
    }

    public async Task RegisterAsync(User user)
    {
        // Serialize the User object to JSON

        var userAsJson = JsonSerializer.Serialize(user);
        StringContent content = new(userAsJson, Encoding.UTF8, "application/json");

        // Send a POST request to the registration endpoint

        var response = await client.PostAsync("https://localhost:7092/auth/register", content);
        var responseContent = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode) throw new Exception(responseContent);
    }

    public Task<ClaimsPrincipal> GetAuthAsync()
    {
        // Create a ClaimsPrincipal based on the stored JWT token

        var principal = CreateClaimsPrincipal();
        return Task.FromResult(principal);
    }

    // Helper method to create a ClaimsPrincipal from the stored JWT token

    private static ClaimsPrincipal CreateClaimsPrincipal()
    {
        if (string.IsNullOrEmpty(Jwt)) return new ClaimsPrincipal();

        var claims = ParseClaimsFromJwt(Jwt);

        ClaimsIdentity identity = new(claims, "jwt");

        ClaimsPrincipal principal = new(identity);
        return principal;
    }
    // Helper method to parse claims from the JWT token

    private static IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
    {
        var payload = jwt.Split('.')[1];
        var jsonBytes = ParseBase64WithoutPadding(payload);
        var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);
        return keyValuePairs!.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString()!));
    }
    // Helper method to parse Base64 without padding

    private static byte[] ParseBase64WithoutPadding(string base64)
    {
        switch (base64.Length % 4)
        {
            case 2:
                base64 += "==";
                break;
            case 3:
                base64 += "=";
                break;
        }

        return Convert.FromBase64String(base64);
    }
}