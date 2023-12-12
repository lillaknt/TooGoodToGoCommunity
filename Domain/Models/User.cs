using System.ComponentModel.DataAnnotations;

namespace Domain.Models;
/// Represents a user entity in the application
public class User
{
    public int Id { get; set; }
    
    [EmailAddress(ErrorMessage = "Invalid email format")] //built in validation for emails
    public string? Email { get; set; }
    public string? FirstName { get; set; }
    public string? Password { get; set; }
    
    public int? PostCode { get; set; }
    
    public int? ItemsPurchased { get; set; }
    public double? CO2Saved { get; set; }

    public User(int id, string? email, string? firstName, string? password, int? postCode,
    int? itemsPurchased, double? co2Saved)
    {
        this.Id = id;
        this.Email = email;
        this.FirstName = firstName;
        this.Password = password;
        this.PostCode = postCode;
         this.ItemsPurchased = itemsPurchased ;
         this.CO2Saved = co2Saved;
    }
   
    public User(){}
    
}
