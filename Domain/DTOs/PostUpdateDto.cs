namespace Domain.DTOs;

public class PostUpdateDto

{
    public int Id { get; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public decimal? Price { get; set; }
    
    public byte[]? ImageData { get; set; }
    
    public int? UserId { get; set; }
    
    public PostUpdateDto(int id, int? userId,  string? title = null, string? description = null, decimal? price = null, byte[]? imageData = null)
    {
        Id = id;
        Title = title;
        Description = description;
        Price = price;
        ImageData = imageData;
        UserId = userId;
    }
}