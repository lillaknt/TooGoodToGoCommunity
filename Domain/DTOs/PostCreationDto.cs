using System.ComponentModel.DataAnnotations;

namespace Domain.DTOs;

public class PostCreationDto
{
    public PostCreationDto(string title, string? description, decimal? price, byte[]? imageData, int userId)

    {
        Title = title;
        Description = description;
        Price = price;
        ImageData = imageData;
        UserId = userId;
    }

    public PostCreationDto()
    {
    }

    [Required(ErrorMessage = "Title is required")]
    public string Title { get; set; }

    public string? Description { get; set; }


    [Range(0, double.MaxValue, ErrorMessage = "Price must be a non-negative value")]
    public decimal? Price { get; set; }

    public byte[]? ImageData { get; set; }

    // Add a property for the user ID
    public int UserId { get; set; }
}