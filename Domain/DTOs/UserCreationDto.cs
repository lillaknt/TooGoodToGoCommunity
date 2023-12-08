namespace Domain.DTOs;

public class UserCreationDto
{
    public UserCreationDto(string email, string firstName, string password, int postCode)
    {
        Email = email;
        FirstName = firstName;
        Password = password;
        PostCode = postCode;
    }

    public string Email { get; }
    public string FirstName { get; }
    public string Password { get; }

    public int PostCode { get; }
}