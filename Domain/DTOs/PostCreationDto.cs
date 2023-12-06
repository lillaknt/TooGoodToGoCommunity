using System.ComponentModel.DataAnnotations;
using Domain.Models;

namespace Domain.DTOs;

public class PostCreationDto
{
    [Required(ErrorMessage = "Title is required")]
    public string Title { get; set; }

    public string? Description { get; set; }

    [Range(0, double.MaxValue, ErrorMessage = "Price must be a non-negative value")]
    public decimal? Price { get; set; }

    // Add a property for the user ID
   // public int UserId { get; set; }
    public User User { get; set; }

    public PostCreationDto(string title, string? description, decimal? price, User user)
    {
        Title = title;
        Description = description;
        Price = price;
        User = user;
    }

    public PostCreationDto() { }
}