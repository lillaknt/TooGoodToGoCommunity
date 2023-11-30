namespace Domain.DTOs;

public class PostCreationDto
{
    public int UserId { get; set; }
    
    public string Title { get; set; }
    public string Body { get; set; }

    public PostCreationDto(int userId, string title, string body)
    {
        UserId = userId;
        
        Title = title;
        Body = body;
    }
}