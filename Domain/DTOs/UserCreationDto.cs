namespace Domain.DTOs;

public class UserCreationDto
{
    public string Email { get; }
    public string Password { get; }

    public UserCreationDto(string email, string password)
    {
        Email = email;
        Password = password;
    }
}