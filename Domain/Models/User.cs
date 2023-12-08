using System.ComponentModel.DataAnnotations;

namespace Domain.Models;

public class User
{
    public User(int id, string? email, string? firstName, string? password, int? postCode)
    {
        Id = id;
        Email = email;
        FirstName = firstName;
        Password = password;
        PostCode = postCode;
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
}