using System.ComponentModel.DataAnnotations;

namespace Domain.DTOs;

public class PostCreationDto
{
    [Required(ErrorMessage = "Title is required")]
    public string Title { get; set; }

    public string? Description { get; set; }
        
    
    [Range(0, double.MaxValue, ErrorMessage = "Price must be a non-negative value")]
    public decimal? Price { get; set; }
    

    public PostCreationDto(string title, string? description, decimal? price)
    {
        Title = title;
        Description = description;
        Price = price;
      
    }

    public PostCreationDto() { }

}