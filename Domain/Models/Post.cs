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

        // Reference to the user who created the post
        //public User Creator { get; set; }
        
        public Post(int id, string title, string? description, decimal? price)
        {
            Id = id;
            Title = title;
            Description = description;
            Price = price;
        }

        /*public Post(int id, string? title, string? description, decimal? price, User creator)
        {
            Id = id;
            Title = title;
            Description = description;
            Price = price;
            Creator = creator;
        }*/

        public Post() { }
    }
}
