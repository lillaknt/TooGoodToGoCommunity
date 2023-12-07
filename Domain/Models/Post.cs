using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class Post
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; }

        public string? Description { get; set; }
        
     
        [Range(0, double.MaxValue, ErrorMessage = "Price must be a non-negative value")]
        public decimal? Price { get; set; }
        
        public byte[]? ImageData { get; set; }

        // Reference to the user who created the post
        public User User { get; set; }
        
        public Post( string title, string? description, decimal? price, byte[]? imageData)
        {
            
            Title = title;
            Description = description;
            Price = price;
            ImageData = imageData;
        }

        public Post(int id, string? title, string? description, decimal? price, User user)
        {
            Id = id;
            Title = title;
            Description = description;
            Price = price;
            User = user;
        }
        
        
        // Parameterless constructor for serialization purposes
        public Post() { }
    }
}
