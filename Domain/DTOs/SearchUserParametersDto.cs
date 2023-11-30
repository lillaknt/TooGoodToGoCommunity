namespace Domain.DTOs;

public class SearchUserParametersDto
{
    public string? EmailContains { get;  }

    public SearchUserParametersDto(string? emailContains)
    {
        EmailContains = emailContains;
    }
}