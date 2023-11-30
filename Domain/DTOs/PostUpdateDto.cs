namespace Domain.DTOs;

public class PostUpdateDto
{
    public int UserId { get; set; }
    public int Id { get; }
    public string? Title { get; set; }
    public string? Body { get; set; }
    public double? Price { get; set; }

    public PostUpdateDto(int id)
    {
        Id = id;
    }
}