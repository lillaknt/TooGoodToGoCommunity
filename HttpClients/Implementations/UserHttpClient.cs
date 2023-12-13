using System.Net.Http.Json;
using System.Text.Json;
using Domain.DTOs;
using Domain.Models;
using HttpClients.ClientInterfaces;

namespace HttpClients.Implementations;

/// Implementation of the HTTP client for interacting with the User service.
public class UserHttpClient : IUserService
{
    private readonly HttpClient client;

    public UserHttpClient(HttpClient client)
    {
        this.client = client;
    }

    public async Task<User> CreateAsync(UserCreationDto dto)
    {
        // Send a POST request to create a new user

        var response = await client.PostAsJsonAsync("/user", dto);
        var result = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode) throw new Exception(result);
        // Deserialize the JSON content to a User object

        var user = JsonSerializer.Deserialize<User>(result, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
        return user;
    }

    public async Task<IEnumerable<User>> GetAsync(string? emailContains = null)
    {
        // Construct the URI based on the optional emailContains parameter

        var uri = "/user";
        if (!string.IsNullOrEmpty(emailContains)) uri += $"?email={emailContains}";
        // Send a GET request to retrieve users based on specified parameters

        var response = await client.GetAsync(uri);
        var result = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode) throw new Exception(result);
        // Deserialize the JSON content to a collection of User objects

        var users = JsonSerializer.Deserialize<IEnumerable<User>>(result, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
        return users;
    }

    public async Task UpdateUserAsync(UserUpdateDto dto)
    {
        // Send a PATCH request to update an existing user

        var response = await client.PatchAsJsonAsync("/user", dto);

        if (!response.IsSuccessStatusCode)
        {
            var result = await response.Content.ReadAsStringAsync();
            throw new Exception(result);
        }
    }
}