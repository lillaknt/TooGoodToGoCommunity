namespace Domain.DTOs;

public class SearchPostParametersDto
{
    public SearchPostParametersDto(int? id, string? title, string? description, decimal? price)
    {
        Id = id;
        Title = title;
        Description = description;
        Price = price;
    }

    public SearchPostParametersDto()
    {
    }

    public int? Id { get; set; }

    public string? Title { get; set; }

    public string? Description { get; set; }

    public decimal? Price { get; set; }
}