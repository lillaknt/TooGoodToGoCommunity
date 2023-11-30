namespace Domain.DTOs;
//this DTO gets the entire post


public class GetPostIdDto

{
    public int Id { get; set; }
  

    public GetPostIdDto(int id)
    {
        Id = id;
    }
}