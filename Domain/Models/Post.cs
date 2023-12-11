using System.ComponentModel.DataAnnotations;

namespace Domain.Models;

public class Post
{
    public Post(string title, string? description, decimal? price, byte[]? imageData, User userid)
    {
        Title = title;
        Description = description;
        Price = price;
        ImageData = imageData;
        User = userid;
    }


    // Parameterless constructor for serialization purposes
    public Post()
    {
    }

    public int Id { get; set; }

    [Required(ErrorMessage = "Title is required")]
    public string Title { get; set; }

    public string? Description { get; set; }


    [Range(0, double.MaxValue, ErrorMessage = "Price must be a non-negative value")]
    public decimal? Price { get; set; }

    public byte[]? ImageData { get; set; }

    // Reference to the user who created the post
    public User User { get; set; }
    
    public string Date { get; set; }
    public string Distance { get; set; }
}