namespace Domain.DTOs;

public class UserCreationDto
{
    public string FirstName { get;}
    public string Password { get; set; }

    public UserCreationDto(string firstName, string password)
    {
        FirstName = firstName;
        Password = password;
    }
}