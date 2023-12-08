namespace Domain.DTOs;

public class SearchUserParametersDto
{
    public SearchUserParametersDto(string? emailContains)
    {
        EmailContains = emailContains;
    }

    public string? EmailContains { get; }
}