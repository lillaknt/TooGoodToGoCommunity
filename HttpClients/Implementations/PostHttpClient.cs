using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using Domain.DTOs;
using Domain.Models;
using HttpClients.ClientInterfaces;

namespace HttpClients.Implementations;

public class PostHttpClient : IPostService
{
    private readonly HttpClient client;

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

        var response = await client.PostAsJsonAsync("/post", dtoWithImageData);
        if (!response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            throw new Exception(content);
        }
    }

    public async Task<ICollection<Post>> GetAsync(string? Title, string? Description, decimal? Price)
    {
        var response = await client.GetAsync("/post");
        var content = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode) throw new Exception(content);

        var posts = JsonSerializer.Deserialize<ICollection<Post>>(content, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
        return posts;
    }


    public async Task<IEnumerable<Post>> GetId(GetPostIdDto id)
    {
        var uri = "";
        if (id.ReturnId() == null)
            uri = "/post";
        else
            uri = $"post?id={id.ReturnId()}";

        var response = await client.GetAsync(uri);
        var result = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode) throw new Exception(result);

        var post = JsonSerializer.Deserialize<IEnumerable<Post>>(result, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
        return post;
    }

    public async Task<PostUpdateDto> GetByIdAsync(int id, int userid)
    {
        var response = await client.GetAsync($"/post/{id}");
        var content = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode) throw new Exception(content);

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
        var dtoAsJson = JsonSerializer.Serialize(dto);
        var body = new StringContent(dtoAsJson, Encoding.UTF8, "application/json");

        var response = await client.PatchAsync("/post", body);
        if (!response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            throw new Exception(content);
        }
    }

    public async Task DeleteAsync(int id)
    {
        var response = await client.DeleteAsync($"post/{id}");
        if (!response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            throw new Exception(content);
        }
    }
}