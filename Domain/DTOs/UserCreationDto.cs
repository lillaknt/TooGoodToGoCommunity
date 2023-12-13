namespace Domain.DTOs;

public class UserCreationDto
{
    public UserCreationDto(string email, string firstName, string password, int postCode, int itemsPurchased,
        double co2Saved)
    {
        Email = email;
        FirstName = firstName;
        Password = password;
        PostCode = postCode;
        ItemsPurchased = itemsPurchased;
        CO2Saved = co2Saved;
    }

    public string Email { get; }
    public string FirstName { get; }
    public string Password { get; }

    public int PostCode { get; }
    public int ItemsPurchased { get; set; }
    public double CO2Saved { get; set; }
}