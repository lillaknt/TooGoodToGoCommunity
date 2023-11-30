namespace Domain.DTOs;

public class PostCreationDto
{
    public int UserId { get; }
    
    public string Title { get; }
    public string Body { get;  }

    public PostCreationDto(int userId, string title, string body)
    {
        UserId = userId;
        
        Title = title;
        Body = body;
    }
}