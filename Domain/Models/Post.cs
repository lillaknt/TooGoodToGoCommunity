namespace Domain.Models;

public class Post
{
    public int Id { get; set; }
    public User User { get; set; }
    public int UserId { get; set; }
    public string FirstName { get; set; }
    public int PostCode { get; set; }
    public string Title { get; set; }
    public string Body { get; set; }
    public double Price { get; set; }

    public Post(int id, User user, int userId, string firstName, int postCode, string title, string? body, double price)
    {
        Id = id;
        User = user;
        UserId = user.Id;
        FirstName = user.FirstName;
        PostCode = user.PostCode;
        Title = title;
        Body = body;
        Price = price;
    }
    
    private Post(){}


    
    
}