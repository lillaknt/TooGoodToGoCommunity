namespace Domain.DTOs;

public class SearchPostParametersDto
{
    public string? TitleContains { get;}
    public string? BodyContains { get;}

    public SearchPostParametersDto(string? titleContains, string? bodyContains)
    {
        
        TitleContains = titleContains;
        BodyContains = bodyContains;
        
    }
}