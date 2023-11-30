namespace Domain.Models;

public class Post
{
    public int Id { get; set; }
    
    public int UserId { get; set; }
    public string? FirstName { get; set; }
    public int? PostCode { get; set; }
    public string Title { get; set; }
    public string? Body { get; set; }
    public double? Price { get; set; }

    public Post(User user, string title, string? body, double? price)
    {
        
        UserId = user.Id;
        FirstName = user.FirstName;
        PostCode = user.PostCode;
        Title = title;
        Body = body;
        Price = price;
    }
    
    private Post(){}


    
    
}