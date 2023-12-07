namespace Domain.DTOs;

public class UserUpdateDto
{
    public string Email { get;  }
    public string FirstName { get; set; }
    public string Password { get;  }
    
    public int PostCode { get; set; }

    public UserUpdateDto(string email, string firstName, string password, int postCode)
    {
        Email = email;
        FirstName = firstName;
        Password = password;
        PostCode = postCode;
    }
}