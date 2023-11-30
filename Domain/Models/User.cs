namespace Domain.Models;

public class User
{
    public int Id { get; set; }
    public string? Email { get; set; }
    public string? FirstName { get; set; }
    public string? Password { get; set; }
    
    public int? PostCode { get; set; }

    public User(int id, string? email, string? firstName, string? password, int? postCode)
    {
        this.Id = id;
        this.Email = email;
        this.FirstName = firstName;
        this.Password = password;
        this.PostCode = postCode;
    }
    
    public User(){}
    
}
