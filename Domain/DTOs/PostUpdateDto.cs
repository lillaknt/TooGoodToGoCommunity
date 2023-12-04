namespace Domain.DTOs;

public class PostUpdateDto

{
    public int Id { get; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public decimal? Price { get; set; }
    
    public PostUpdateDto(int id)
    {
        Id = id;
    }
}