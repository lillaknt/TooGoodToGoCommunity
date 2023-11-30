namespace Domain.Models;

public class User
{
    public int Id { get; set; }
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string Password { get; set; }
    
    public int PostCode { get; set; }

    public User(string email, string? firstName, string? password, int postCode)
    {
        this.Email = email;
        this.FirstName = firstName;
        this.Password = password;
        this.PostCode = postCode;
    }
    
    public User(){}
    
}
