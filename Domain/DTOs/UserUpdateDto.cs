namespace Domain.DTOs;

public class UserUpdateDto
{
    public string Email { get; set; }
    public string FirstName { get; set; }
    
    public int? PostCode { get; set; }
    public int ItemsPurchased { get; set; }
    public double CO2Saved { get; set; }

    public UserUpdateDto(string email, string firstName, int postCode, int itemsPurchased, double co2Saved )
    {
        Email = email;
        FirstName = firstName;
        PostCode = postCode;
        ItemsPurchased = itemsPurchased;
        CO2Saved = co2Saved;

    }
    public UserUpdateDto(){}
}