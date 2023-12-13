using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using Domain.DTOs;
using Domain.Models;
using HttpClients.ClientInterfaces;

namespace HttpClients.Implementations;

/// Implementation of the HTTP client for interacting with the Post service.
public class PostHttpClient : IPostService
{
    private readonly HttpClient client;

    /// Constructor to initialize the HTTP client.
    public PostHttpClient(HttpClient client)
    {
        this.client = client;
    }

    public async Task CreateAsync(PostCreationDto dto)
    {
        // Convert the image data to base64 before sending it to the server
        var base64ImageData = dto.ImageData != null ? Convert.ToBase64String(dto.ImageData) : null;
        // Include the base64-encoded image data in the DTO
        var dtoWithImageData = new
        {
            dto.Title,
            dto.Description,
            dto.Price,
            ImageData = base64ImageData,
            dto.UserId
        };
        // Send a POST request to create a new post

        var response = await client.PostAsJsonAsync("/post", dtoWithImageData);
        if (!response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            throw new Exception(content);
        }
    }

    public async Task<ICollection<Post>> GetAsync(string? Title, string? Description, decimal? Price)
    {
        // Send a GET request to retrieve posts based on specified parameters

        var response = await client.GetAsync("/post");
        var content = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode) throw new Exception(content);

        // Deserialize the JSON content to a collection of Post objects

        var posts = JsonSerializer.Deserialize<ICollection<Post>>(content, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
        return posts;
    }


    public async Task<IEnumerable<Post>> GetId(GetPostIdDto id)
    {
        // Determine the URI based on whether an ID is provided

        var uri = "";
        if (id.ReturnId() == null)
            uri = "/post";
        else
            uri = $"post?id={id.ReturnId()}";

        // Send a GET request to retrieve posts based on ID

        var response = await client.GetAsync(uri);
        var result = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode) throw new Exception(result);

        // Deserialize the JSON content to a collection of Post objects

        var post = JsonSerializer.Deserialize<IEnumerable<Post>>(result, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
        return post;
    }

    public async Task<PostUpdateDto> GetByIdAsync(int id, int userid)
    {
        // Send a GET request to retrieve a specific post by ID

        var response = await client.GetAsync($"/post/{id}");
        var content = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode) throw new Exception(content);

        // Deserialize the JSON content to a PostUpdateDto object

        var dto = JsonSerializer.Deserialize<PostUpdateDto>(content,
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            }
        )!;
        return dto;
    }


    public async Task UpdateAsync(PostUpdateDto dto)
    {
        // Serialize the PostUpdateDto to JSON

        var dtoAsJson = JsonSerializer.Serialize(dto);
        var body = new StringContent(dtoAsJson, Encoding.UTF8, "application/json");
        // Send a PATCH request to update an existing post

        var response = await client.PatchAsync("/post", body);
        if (!response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            throw new Exception(content);
        }
    }

    public async Task DeleteAsync(int id)
    {
        // Send a DELETE request to delete a post by ID

        var response = await client.DeleteAsync($"post/{id}");
        if (!response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            throw new Exception(content);
        }
    }
}