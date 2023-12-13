using System.ComponentModel.DataAnnotations;

namespace Domain.Models;

/// Represents a user entity in the application
public class User
{
    public User(int id, string? email, string? firstName, string? password, int? postCode,
        int? itemsPurchased, double? co2Saved)
    {
        Id = id;
        Email = email;
        FirstName = firstName;
        Password = password;
        PostCode = postCode;
        ItemsPurchased = itemsPurchased;
        CO2Saved = co2Saved;
    }

    public User()
    {
    }

    public int Id { get; set; }

    [EmailAddress(ErrorMessage = "Invalid email format")] //built in validation for emails
    public string? Email { get; set; }

    public string? FirstName { get; set; }
    public string? Password { get; set; }

    public int? PostCode { get; set; }

    public int? ItemsPurchased { get; set; }
    public double? CO2Saved { get; set; }
}